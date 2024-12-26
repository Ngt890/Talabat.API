using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using StackExchange.Redis;
using System.Text.Json;
using Microsoft.VisualBasic;

namespace Talabat.Repository
{
    public class BasketRepository:IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis )
        {
            _database = redis.GetDatabase();
        }

         public async Task<bool>DeleteBasketAsync(string id)
        {
           return await _database.KeyDeleteAsync(id);   
        }

        public async Task<CustomerBasket?> GetBasketAsync(string  id)
        {
            var Basket= await _database.StringGetAsync(id);
             
            return Basket.IsNull ?null: JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public  async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var json =   JsonSerializer.Serialize(basket);
            var Updatedbasket = _database.StringSetAsync(basket.Id, json, TimeSpan.FromDays(1));
            if (Updatedbasket is null ) return null;
            return   await GetBasketAsync(basket.Id);
        }

        
    }
}
