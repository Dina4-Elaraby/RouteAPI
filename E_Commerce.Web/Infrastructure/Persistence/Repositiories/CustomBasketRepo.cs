using DomainLayer.Models.BasketModule;
using DomainLayer.RepoInterface;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System.Text.Json;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Persistence.Repositiories
{
    public class CustomBasketRepo(IConnectionMultiplexer connection) : ICustomBasketRepo
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomBasket?> CreateOrUpdateBasketAsync(CustomBasket customBasket, TimeSpan? TimeToLive = null)
        {
            //now we wanna make serializer convert from #c object to string
            var JsonBasket = JsonSerializer.Serialize(customBasket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(customBasket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
                return await GetBasketAsync(customBasket.Id);
            else
                return null;
        }

        public async Task<bool> DeleteBasketAsync(string Id)
        {
            return await _database.KeyDeleteAsync(Id);
        }

        public async Task<CustomBasket?> GetBasketAsync(string Id)
        {
            var Basket = await _database.StringGetAsync(Id);
            if (Basket.IsNullOrEmpty) 
                return null;
            else
                return JsonSerializer.Deserialize<CustomBasket>(Basket);
        }
    }
}
