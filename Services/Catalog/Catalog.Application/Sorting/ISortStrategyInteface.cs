namespace Catalog.Application.Sorting;
public interface ISortStrategyInteface
{
    ISortStrategy GetSortStrategy(string sortOption);
}
