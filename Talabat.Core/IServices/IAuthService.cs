using Talabat.Core.Entities.Identity;

namespace Talabat.Core.IServices
{
    public interface IAuthService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
