using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.IServices
{
    public interface IPaymentService
    {
        Task<Order?> CreateOrUpdatePaymentIntent(int orderId,string userEmail);
    }
}
