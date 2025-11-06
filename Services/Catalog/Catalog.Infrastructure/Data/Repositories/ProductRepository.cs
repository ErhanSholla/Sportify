using AutoMapper;
using Catalog.Core.Entities;
using Catalog.Core.Repository;
using Catalog.Core.Specification;
using Catalog.Infrastructure.Documents;
using Catalog.Infrastructure.Sorting;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;
        private readonly MongoSortStrategyFactory<ProductDocument> _sortStrategyFactory;
        private readonly Mapper _mapper;
        private static readonly Collation _caseInsensitiveCollation = new Collation("en", strength: CollationStrength.Secondary);

        public ProductRepository(ICatalogContext catalogContext, MongoSortStrategyFactory<ProductDocument> sortStrategyFactory, Mapper mapper)
        {
            ArgumentNullException.ThrowIfNull(catalogContext, nameof(catalogContext));
            ArgumentNullException.ThrowIfNull(sortStrategyFactory, nameof(sortStrategyFactory));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _catalogContext = catalogContext;
            _sortStrategyFactory = sortStrategyFactory;
            _mapper = mapper;
        }
        async Task<Product> IProductRepository.CreateProduct(Product product)
        {
            var document = _mapper.Map<ProductDocument>(product);
            await _catalogContext.Products.InsertOneAsync(document);

            return _mapper.Map<Product>(document);
        }

        async Task<bool> IProductRepository.DeleteProduct(string id)
        {
            var deletedProduct = await _catalogContext.Products.DeleteOneAsync(p => p.Id.Equals(id));
            return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
        }


        public async Task<Pagination<Product>> GetAllProducts(CatalogSpecifcationParam catalogSpecifcation)
        {
            var builder = Builders<ProductDocument>.Filter;
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
            var mappedData = _mapper.Map<IReadOnlyList<Product>>(data);

            return new Pagination<Product>(catalogSpecifcation.PageIndex, catalogSpecifcation.PageSize, (int)totalItems, mappedData);
        }

        private async Task<IReadOnlyList<ProductDocument>> DataFilter(CatalogSpecifcationParam catalogSpecifcation, FilterDefinition<ProductDocument> filter)
        {
            var sortBuilder = Builders<ProductDocument>.Sort;
            var strategy = _sortStrategyFactory.GetStrategy(catalogSpecifcation.Sort ?? "name");

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
            var document = await _catalogContext.Products.Find(p => p.Id.Equals(id)).FirstOrDefaultAsync();

            return _mapper.Map<Product>(document);
        }

        public async Task<IEnumerable<Product>> GetProductsByBrandName(string brandName)
        {
            var fitler = Builders<ProductDocument>.Filter.Eq(p => p.Brand.Name, brandName);
            var options = new FindOptions<ProductDocument>
            {
                Collation = _caseInsensitiveCollation,
            };

            using var cursor = await _catalogContext.Products.FindAsync(fitler, options);
            var documents = await cursor.ToListAsync();

            return _mapper.Map<IEnumerable<Product>>(documents);
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            var fitler = Builders<ProductDocument>.Filter.Eq(p => p.Name, name);
            var options = new FindOptions<ProductDocument>
            {
                Collation = _caseInsensitiveCollation,
            };
            using var cursor = await _catalogContext.Products.FindAsync(fitler, options);
            var documents = await cursor.ToListAsync();

            return _mapper.Map<IEnumerable<Product>>(documents);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var document = _mapper.Map<ProductDocument>(product);
            var updatedProduct = await _catalogContext.Products.ReplaceOneAsync(p => p.Id.Equals(product.Id), document);

            return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
        }
    }
}
