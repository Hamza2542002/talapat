using Talabat.Core.Specifications;

namespace Talabat.Extentions
{
    public static class SpecificationConfiguration
    {
        public static IServiceCollection AddSpecificService(this IServiceCollection services)
        {
            return services.AddScoped(typeof(ISpecifications<>), typeof(BaseSpecifications<>));
        }
    }
}
