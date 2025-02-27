using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.Extentions
{
    public static class SeedingExtention
    {

        public async static Task<IApplicationBuilder> UseSeedingData(this IApplicationBuilder app , IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();
            var _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var _identityContext = services.GetRequiredService<ApplicationIdentityDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(typeof(Program));
            try
            {
                await _dbContext.Database.MigrateAsync();
                await _identityContext.Database.MigrateAsync();
                await StoreSeedingContext.SeedAsync(_dbContext);
                await IdentitySeedingContext.SeedUsersAsync(_userManager);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return app;
        }
    }
}
