using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Specifications.OrderSpecs;

namespace Talabat.Core.IServices
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string customerEmail, string BasketId, int deleveryMethodId, Address orderAddress);
        Task<IReadOnlyList<Order>?> GetOrdersForUserAsync(string userEmail, OrderSpecsParams specsParams);
        Task<Order?> GetOrderForUserAsync(int OrderId , string userEmail);
        Task<IReadOnlyList<DeleveryMethod>?> GetDeleveryMethods();
    }
}
