using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Application.Sorting;
public class PriceSortStrategy : ISortStrategy
{
    public string key => "priceAsc";

    public SortDefinition<Product> ApplySort(SortDefinitionBuilder<Product> builder)
    {
        return builder.Ascending(p => p.Price);
    }
}

