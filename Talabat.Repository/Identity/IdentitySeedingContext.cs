using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class IdentitySeedingContext
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> _userManager)
        {
            if (!_userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    DisplayName = "Hamza",
                    Email = "mezo2542002@gmail.com",
                    UserName = "hamza",
                    PhoneNumber = "01275869193"
                };

                await _userManager.CreateAsync(user, "Hamza123_");
            }

        }
        private static async Task SeedRolesAsync(ApplicationIdentityDbContext context)
        {

        }
    }
}
