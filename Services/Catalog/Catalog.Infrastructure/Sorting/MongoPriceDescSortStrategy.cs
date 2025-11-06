using Catalog.Infrastructure.Documents;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
