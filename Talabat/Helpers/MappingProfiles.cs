using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Dtos;

namespace Talabat.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Product, ProductToreturnDTO>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PicUrl, o => o.MapFrom<ProductUrlPicResolver>());


            CreateMap<CartItemDTO,CartItem>();
            CreateMap<CustomerCartDTO, CustomerCart>();
            CreateMap<AddressDTO, Address>();
        }
    }
}
