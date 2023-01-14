using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.BLL.Entities.Order
{
    public class Order:BaseEntity
    {
        public Order()
        {
        }

        public Order(string buyerEmail, Address shipedToAdress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems, decimal subtotal , string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShipedToAdress = shipedToAdress;
            DeliveryMethod = deliveryMethod;
            this.OrderItems = orderItems;
            Subtotal = subtotal;

            PaymentIntentId = paymentIntentId;
        }

        public int Id { get; set; }
        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Address ShipedToAdress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }

        public IReadOnlyList<OrderItem> OrderItems { get; set; }

        public decimal Subtotal { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string PaymentIntentId { get; set; }
         public decimal GetTotal()
                 => Subtotal + DeliveryMethod.Price;
    }
}
