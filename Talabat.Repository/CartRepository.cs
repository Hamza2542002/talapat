using StackExchange.Redis;
using System.Collections.Generic;
using System.Text.Json;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;

namespace Talabat.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _database;

        public CartRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<bool> DeleteCustomerCartAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerCart?> GetCustomerCartAsync(string id)
        {
            var cart = await _database.StringGetAsync(id);

            return JsonSerializer.Deserialize<CustomerCart>(cart);
        }

        public async Task<CustomerCart?> UpdateCustomerCartAsync(CustomerCart cart)
        {
            var newCart = cart;
            var result = await _database.StringSetAsync(cart.Id, 
                JsonSerializer.Serialize(cart),TimeSpan.FromDays(30));
            
            if(!result)
                return null;

            return newCart;
        }
    }
}
