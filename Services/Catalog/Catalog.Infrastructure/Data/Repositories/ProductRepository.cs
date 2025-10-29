using Catalog.Application.Sorting;
using Catalog.Core.Entities;
using Catalog.Core.Repository;
using Catalog.Core.Specification;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;
        private readonly ISortStrategyInteface _sortStrategyFactory;

        public ProductRepository(ICatalogContext catalogContext, ISortStrategyInteface sortStrategyFactory)
        {
            ArgumentNullException.ThrowIfNull(catalogContext, nameof(catalogContext));
            ArgumentNullException.ThrowIfNull(sortStrategyFactory, nameof(sortStrategyFactory));
            _catalogContext = catalogContext;
            _sortStrategyFactory = sortStrategyFactory;
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


        public async Task<Pagination<Product>> GetAllProducts(CatalogSpecifcationParam catalogSpecifcation)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(catalogSpecifcation.Search))
            {
                // this return a new combined filter. it doesnt mutate the original filter
                filter = filter & builder.Where(p => p.Name.ToLower().Contains(catalogSpecifcation.Search.ToLower()));
            }
            if (!string.IsNullOrEmpty(catalogSpecifcation.BrandId))
            {
                var brandFilter = builder.Eq(p => p.Brand.Id, catalogSpecifcation.BrandId);
                // Give Me a product where BRandId=X, in addition to any previous conditions
                filter &= brandFilter; // equivalent to filter = filter & brandFilter

            }
            if (!string.IsNullOrEmpty(catalogSpecifcation.TypeId))
            {
                var typeFilter = builder.Eq(p => p.Type.Id, catalogSpecifcation.TypeId);

                filter &= typeFilter;
            }

            var totalItems = await _catalogContext.Products.CountDocumentsAsync(filter);
            var data = await DataFilter(catalogSpecifcation, filter);

            return new Pagination<Product>(catalogSpecifcation.PageIndex, catalogSpecifcation.PageSize, (int)totalItems, data);
        }

        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecifcationParam catalogSpecifcation, FilterDefinition<Product> filter)
        {
            var sortBuilder = Builders<Product>.Sort;
            var strategy = _sortStrategyFactory.GetSortStrategy(catalogSpecifcation.Sort ?? "name");

            var sortDefinition = strategy.ApplySort(sortBuilder);

            return await _catalogContext.Products
                .Find(filter)
                .Sort(sortDefinition)
                .Skip(catalogSpecifcation.PageSize * (catalogSpecifcation.PageIndex - 1))
                .Limit(catalogSpecifcation.PageSize)
                .ToListAsync();
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
