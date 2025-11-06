using Catalog.Core.Entities;
using Catalog.Infrastructure.Documents;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Brands = database.GetCollection<ProductBrandDocument>(configuration.GetValue<string>("DatabaseSettings:BrandsCollection"));
            Types = database.GetCollection<ProductTypeDocument>(configuration.GetValue<string>("DatabaseSettings:TypesCollection"));
            Products = database.GetCollection<ProductDocument>(configuration.GetValue<string>("DatabaseSettings:ProductsCollection"));

            BrandContextSeed.SeedData(Brands);
            TypeContextSeed.SeedData(Types);
            CatalogContextSeed.SeedData(Products);


        }
        public IMongoCollection<ProductDocument> Products { get; }

        public IMongoCollection<ProductBrandDocument> Brands { get; }

        public IMongoCollection<ProductTypeDocument> Types { get; }
    }
}
