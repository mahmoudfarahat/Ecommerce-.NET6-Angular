using Ecom.BLL.Entities;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Dto
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }

        public List<BasketItem> Items { get; set; }  
        public int? DelivertMethod { get; set; }

        public decimal ShippingPrice { get; set; }
    }
}
