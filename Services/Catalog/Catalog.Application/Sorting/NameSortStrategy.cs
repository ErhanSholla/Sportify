using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Application.Sorting;
public class NameSortStrategy : ISortStrategy
{
    public string key => "name";

    public SortDefinition<Product> ApplySort(SortDefinitionBuilder<Product> builder)
    {
        return builder.Ascending(x => x.Name);
    }
}

