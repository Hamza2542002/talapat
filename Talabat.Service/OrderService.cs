using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IServices;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        public Task<Order> CreateOrderAsync(string customerEmail, string BasketId, int deleveryMethodId, Address orderAddress)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderForUserAsync(int OrderId, string userEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
