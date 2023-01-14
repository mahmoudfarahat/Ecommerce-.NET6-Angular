using Ecom.BLL.Entities.Order;

namespace ecommerce.Dto
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; }
        public AddressDto ShipedToAdress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }

        public decimal Subtotal { get; set; }

        public string Status { get; set; }

        public decimal Total { get; set; }
    }
}
