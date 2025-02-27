using Talabat.Helpers;

namespace Talabat.Extentions
{
    public static class MapperConfiguraion
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));
        }
        public static IServiceCollection AddResolvers(this IServiceCollection services)
        {
            services.AddScoped<ProductUrlPicResolver>();

            return services;
        }
    }
}
