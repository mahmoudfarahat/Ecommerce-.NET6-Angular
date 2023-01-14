using AutoMapper;
using Ecom.BLL.Entities;
using Ecom.BLL.Entities.Order;
using ecommerce.Dto;

namespace ecommerce.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {

            CreateMap<Product, ProductDTo>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                 .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                 .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>());

            CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            CreateMap<AddressDto, Ecom.BLL.Entities.Order.Address>().ReverseMap();

            CreateMap<Order, OrderDetailsDto>()
                .ForMember(dest => dest.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, opt => opt.MapFrom(src => src.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(dest => dest.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(dest => dest.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                                 .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemUrlResolver>());


        }

    }
}
