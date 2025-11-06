using Catalog.Infrastructure.Documents;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Sorting
{
    public class MongoPriceAscSortStrategy : IMongoSortStrategy<ProductDocument>
    {
        public string Key => "priceAsc";

        public SortDefinition<ProductDocument> ApplySort(SortDefinitionBuilder<ProductDocument> sortDefinition)
        {
            return sortDefinition.Ascending(p => p.Price);
        }
    }
}
