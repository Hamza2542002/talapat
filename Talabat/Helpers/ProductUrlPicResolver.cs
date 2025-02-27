using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Dtos;

namespace Talabat.Helpers
{
    public class ProductUrlPicResolver : IValueResolver<Product, ProductToreturnDTO, string>
    {
        private readonly IConfiguration _configuration;
            
        public ProductUrlPicResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToreturnDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PicUrl))
            {
                return $"{_configuration["BaseUrl"]}/{source.PicUrl}";
            }
            return string.Empty;
        }
    }
}
