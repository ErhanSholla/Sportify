using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Application.Sorting;
public interface ISortStrategy
{
    string key { get; }
    SortDefinition<Product> ApplySort(SortDefinitionBuilder<Product> builder);
}

