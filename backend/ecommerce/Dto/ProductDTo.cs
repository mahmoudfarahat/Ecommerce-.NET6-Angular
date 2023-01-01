using Ecom.BLL.Entities;

namespace ecommerce.Dto
{
    public class ProductDTo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
      
        public string ProductType { get; set; }

       

        public string ProductBrand { get; set; }
    }
}
