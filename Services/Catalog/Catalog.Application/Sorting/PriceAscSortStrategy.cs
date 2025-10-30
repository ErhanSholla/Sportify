using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Application.Sorting;
public class PriceAscSortStrategy : ISortStrategy
{
    public string key => "priceDesc";

    public SortDefinition<Product> ApplySort(SortDefinitionBuilder<Product> builder)
    {
        return builder.Descending(x => x.Price);
    }
}

