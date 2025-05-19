using Stripe;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Specifications.OrderSpecs;

using OrderAddress = Talabat.Core.Entities.Order_Aggregate.Address;

namespace Talabat.Core.IServices
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string customerEmail, string BasketId, int deleveryMethodId, OrderAddress orderAddress);
        Task<IReadOnlyList<Order>?> GetOrdersForUserAsync(string userEmail, OrderSpecsParams specsParams);
        Task<Order?> GetOrderForUserAsync(int OrderId , string userEmail);
        Task<IReadOnlyList<DeleveryMethod>?> GetDeleveryMethods();
        Task<bool> UpdateOrderState(string paymentIntent,OrderStatus status);
    }
}
