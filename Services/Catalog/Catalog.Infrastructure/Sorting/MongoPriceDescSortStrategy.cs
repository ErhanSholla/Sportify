using Catalog.Infrastructure.Documents;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Sorting
{
    public class MongoPriceDescSortStrategy : IMongoSortStrategy<ProductDocument>
    {
        public string Key => "priceDesc";

        public SortDefinition<ProductDocument> ApplySort(SortDefinitionBuilder<ProductDocument> sortDefinition)
        {
            return sortDefinition.Descending(p => p.Price);
        }
    }
}
