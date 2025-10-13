using Catalog.Core.Entities;
using Catalog.Core.Repository;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepo, ITypesRepo
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            ArgumentNullException.ThrowIfNull(catalogContext, nameof(catalogContext));
            _catalogContext = catalogContext;

        }
        async Task<Product> IProductRepository.CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
            return product;
        }

        async Task<bool> IProductRepository.DeleteProduct(string id)
        {
            var deletedProduct = await _catalogContext.Products.DeleteOneAsync(p => p.Id.Equals(id));
            return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _catalogContext.Brands.Find(b => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _catalogContext.Products.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _catalogContext.Types.Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _catalogContext.Products.Find(p => p.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByBrandName(string brandName)
        {
            return await _catalogContext.Products.Find(p => p.Brand.Name.ToLower().Equals(brandName.ToLower())).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _catalogContext.Products.Find(p => p.Name.ToLower().Equals(name.ToLower())).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updatedProduct = await _catalogContext.Products.ReplaceOneAsync(p => p.Id.Equals(product.Id), product);
            return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
        }
    }
}
