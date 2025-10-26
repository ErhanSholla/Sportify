using Catalog.Core.Entities;
using Catalog.Core.Specification;

namespace Catalog.Core.Repository
{
    public interface IProductRepository
    {
        public Task<Pagination<Product>> GetAllProducts(CatalogSpecifcationParam catalogSpecifcation);
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByBrandName(string brandName);
        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
