namespace Catalog.Infrastructure.Sorting
{
    public class MongoSortStrategyFactory<T>
    {
        private readonly IEnumerable<IMongoSortStrategy<T>> _strategies;

        public MongoSortStrategyFactory(IEnumerable<IMongoSortStrategy<T>> strategies)
        {
            _strategies = strategies;
        }

        public IMongoSortStrategy<T> GetStrategy(string sortOptions)
        {
            if (string.IsNullOrEmpty(sortOptions))
            {
                sortOptions = "name";
            }

            var strategies = _strategies.FirstOrDefault(s => s.Key.Equals(sortOptions, StringComparison.OrdinalIgnoreCase));

            return strategies ?? _strategies.First(s => s.Key == "name");
        }
    }
}
