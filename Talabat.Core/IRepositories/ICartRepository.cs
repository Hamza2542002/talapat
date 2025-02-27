using Talabat.Core.Entities;

namespace Talabat.Core.IRepositories
{
    public interface ICartRepository
    {
        Task<CustomerCart?> GetCustomerCartAsync(string id);
        Task<CustomerCart?> UpdateCustomerCartAsync(CustomerCart cart);
        Task<bool> DeleteCustomerCartAsync(string id);
    }
}
