using Catalog.Core.Entities;

namespace Catalog.Core.Repository
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByBrandName(string brandName);
        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
