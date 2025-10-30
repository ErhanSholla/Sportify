namespace Catalog.Application.Sorting
{
    public class SortStrategyFactory : ISortStrategyInteface
    {
        private readonly IEnumerable<ISortStrategy> _strategies;

        public SortStrategyFactory(IEnumerable<ISortStrategy> strategies)
        {
            _strategies = strategies;
        }
        public ISortStrategy GetSortStrategy(string sortOption)
        {
            if (string.IsNullOrEmpty(sortOption))
            {
                sortOption = "name";
            }
            var strategies = _strategies.FirstOrDefault(s => s.key.Equals(sortOption, StringComparison.OrdinalIgnoreCase));

            return strategies ?? _strategies.First(s => s.key == "name");
        }
    }
}
