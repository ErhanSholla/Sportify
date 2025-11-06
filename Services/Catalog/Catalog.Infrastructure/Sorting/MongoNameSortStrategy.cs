using Catalog.Infrastructure.Documents;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Sorting
{
    public class MongoNameSortStrategy : IMongoSortStrategy<ProductDocument>
    {
        public string Key => "name";

        public SortDefinition<ProductDocument> ApplySort(SortDefinitionBuilder<ProductDocument> sortDefinition)
        {
            return sortDefinition.Ascending(p => p.Name);
        }
    }
}
