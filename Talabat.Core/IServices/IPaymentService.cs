using Talabat.Core.Entities;

namespace Talabat.Core.IServices
{
    public interface IPaymentService
    {
        Task<CustomerCart?> CreateOrUpdatePaymentIntent(string cartId);
    }
}
