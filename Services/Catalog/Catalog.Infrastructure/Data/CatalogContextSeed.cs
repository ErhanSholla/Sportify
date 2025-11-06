using Catalog.Core.Entities;
using Catalog.Infrastructure.Documents;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;
public static class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<ProductDocument> productCollection)
    {
        bool checkProducts = productCollection.Find(b => true).Any();

        // Current working directory (changes depending on environment)
        string basePath = Directory.GetCurrentDirectory();

        // Default path (Docker or root run)
        string path = Path.Combine("Data", "SeedData", "products.json");

        if (!File.Exists(path))
        {
            path = Path.Combine(Directory.GetParent(basePath)!.FullName,
                                "Catalog.Infrastructure", "Data", "SeedData", "products.json");
        }

        if (!checkProducts)
        {
            var productsData = File.ReadAllText(path);
            var products = JsonSerializer.Deserialize<List<ProductDocument>>(productsData);

            if (products != null)
            {
                foreach (var product in products)
                {
                    productCollection.InsertOneAsync(product);
                }
            }
        }
    }
}

