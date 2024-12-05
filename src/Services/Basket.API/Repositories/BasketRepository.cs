using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _cache;
        private readonly ISerializeService _serializeService;
        private readonly ILogger _logger;

        public BasketRepository(IDistributedCache cache, ISerializeService serializeService, ILogger logger)
        {
            _cache = cache;
            _serializeService = serializeService;
            _logger = logger;
        }

        public async Task<Cart?> GetBasketByUserName(string userName)
        {
            _logger.Information($"BEGIN: {nameof(GetBasketByUserName)} {userName}");
            var basket = await _cache.GetStringAsync(userName);
            _logger.Information($"END: {nameof(GetBasketByUserName)} {userName}");

            return string.IsNullOrEmpty(basket) ? null : _serializeService.Deserialize<Cart>(basket);
        }

        public async Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options)
        {
            _logger.Information($"BEGIN: {nameof(UpdateBasket)} for {cart.Username}");
            if (options != null)
            {
                await _cache.SetStringAsync(cart.Username, _serializeService.Serialize(cart), options);
            }
            else 
            {
                await _cache.SetStringAsync(cart.Username, _serializeService.Serialize(cart));
            }
            _logger.Information($"END: {nameof(UpdateBasket)} for {cart.Username}");

            return await GetBasketByUserName(cart.Username);
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            _logger.Information($"BEGIN: {nameof(DeleteBasket)} for {userName}");
            await _cache.RemoveAsync(userName);
            _logger.Information($"END: {nameof(DeleteBasket)} for {userName}");

            return true;
        }
    }
}
