using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Repository.Data;

namespace Talabat.Extentions
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddStoreDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cs"));
            });
        }
    }
}
