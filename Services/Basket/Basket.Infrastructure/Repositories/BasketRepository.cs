using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisChace;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisChace = redisCache;
        }

        public async Task DeleteBasket(string username)
        {
            await _redisChace.RemoveAsync(username);

        }

        public async Task<ShoppingCart?> GetBasket(string username)
        {
            var basket = await _redisChace.GetStringAsync(username);

            if (string.IsNullOrWhiteSpace(basket))
            {
                return null;
            }

            return JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart cart)
        {
            await _redisChace.SetStringAsync(cart.UserName, JsonSerializer.Serialize<ShoppingCart>(cart));

            return await GetBasket(cart.UserName) ?? cart;
        }




    }
}
