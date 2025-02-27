using Talabat.Core.IRepositories;
using Talabat.Repository;

namespace Talabat.Extentions
{
    public static class RepoConfiguration
    {
        public static IServiceCollection AddGenericRepo(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
