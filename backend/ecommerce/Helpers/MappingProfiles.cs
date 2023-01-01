using AutoMapper;
using Ecom.BLL.Entities;
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


        }

    }
}
