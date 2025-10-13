using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;
public static class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool checkProduct = productCollection.Find(p => true).Any();
        string path = Path.Combine("Data", "SeedData", "products.json");

        if (!checkProduct)
        {
            var productsData = File.ReadAllText(path);
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products is not null)
            {
                foreach (var product in products)
                {
                    productCollection.InsertOneAsync(product);
                }

            }
        }
    }
}

