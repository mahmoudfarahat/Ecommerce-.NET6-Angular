using AutoMapper;
using Ecom.BLL.Entities;
using ecommerce.Dto;

namespace ecommerce.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDTo, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDTo destination, string destMember, ResolutionContext context)
        {
             if(!string.IsNullOrEmpty(source.PictureUrl)) {
            return _configuration["ApiUrl"]+ source.PictureUrl; 
            }

            return null;
        }
    }
}
