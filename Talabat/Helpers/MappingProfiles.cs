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

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.OrderedItemProduct.ProductName))
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.OrderedItemProduct.ProductId))
                .ForMember(d => d.ProductPicUrl, o => o.MapFrom(s => s.OrderedItemProduct.ProductPicUrl));

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(o => o.DeleveryMethodName, o => o.MapFrom(s => s.DeleveryMethod.ShortName))
                .ForMember(o => o.DeleveryMethodCost, o => o.MapFrom(s => s.DeleveryMethod.Cost));

        }
    }
}
