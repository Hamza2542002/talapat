using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.IServices
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string customerEmail, string BasketId, int deleveryMethodId, Address orderAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string userEmail);
        Task<Order> GetOrderForUserAsync(int OrderId , string userEmail);
    }
}
