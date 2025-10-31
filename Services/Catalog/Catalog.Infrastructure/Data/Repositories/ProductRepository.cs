using Catalog.Application.Sorting;
using Catalog.Core.Entities;
using Catalog.Core.Repository;
using Catalog.Core.Specification;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;
        private readonly ISortStrategyInteface _sortStrategyFactory;
        private static readonly Collation _caseInsensitiveCollation = new Collation("en", strength: CollationStrength.Secondary);

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
                // flexible substring search (case-insensitive)
                var re = new BsonRegularExpression(catalogSpecifcation.Search, "i");
                filter &= builder.Regex(p => p.Name, re);
            }
            if (!string.IsNullOrEmpty(catalogSpecifcation.BrandId))
            {
                filter &= builder.Eq(p => p.Brand.Id, catalogSpecifcation.BrandId);
            }
            if (!string.IsNullOrEmpty(catalogSpecifcation.TypeId))
            {
                filter &= builder.Eq(p => p.Type.Id, catalogSpecifcation.TypeId);
            }

            var countOptions = new CountOptions
            {
                Collation = _caseInsensitiveCollation
            };

            var totalItems = await _catalogContext.Products.CountDocumentsAsync(filter, countOptions);
            var data = await DataFilter(catalogSpecifcation, filter);

            return new Pagination<Product>(catalogSpecifcation.PageIndex, catalogSpecifcation.PageSize, (int)totalItems, data);
        }

        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecifcationParam catalogSpecifcation, FilterDefinition<Product> filter)
        {
            var sortBuilder = Builders<Product>.Sort;
            var strategy = _sortStrategyFactory.GetSortStrategy(catalogSpecifcation.Sort ?? "name");

            var sortDefinition = strategy.ApplySort(sortBuilder);

            var aggregateOptions = new AggregateOptions
            {
                Collation = _caseInsensitiveCollation
            };

            var query = _catalogContext.Products
                .Aggregate(aggregateOptions)
                .Match(filter)
                .Sort(sortDefinition)
                .Skip(catalogSpecifcation.PageSize * (catalogSpecifcation.PageIndex - 1))
                .Limit(catalogSpecifcation.PageSize);
            return await query.ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _catalogContext.Products.Find(p => p.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByBrandName(string brandName)
        {
            var fitler = Builders<Product>.Filter.Eq(p => p.Brand.Name, brandName);
            var options = new FindOptions<Product>
            {
                Collation = _caseInsensitiveCollation,
            };

            using var cursor = await _catalogContext.Products.FindAsync(fitler, options);
            return await cursor.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            var fitler = Builders<Product>.Filter.Eq(p => p.Name, name);
            var options = new FindOptions<Product>
            {
                Collation = _caseInsensitiveCollation,
            };
            using var cursor = await _catalogContext.Products.FindAsync(fitler, options);
            return await cursor.ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updatedProduct = await _catalogContext.Products.ReplaceOneAsync(p => p.Id.Equals(product.Id), product);
            return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
        }
    }
}
