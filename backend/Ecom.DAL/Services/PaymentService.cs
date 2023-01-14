using Ecom.BLL.Entities;
using Ecom.BLL.Entities.Order;
using Ecom.BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Ecom.BLL.Entities.Product;

namespace Ecom.DAL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaskedRepository _baskedRepository;
        private readonly IConfiguration _configuration;

        public PaymentService(IUnitOfWork unitOfWork ,
              IBaskedRepository baskedRepository,
              IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
           _baskedRepository = baskedRepository;
           _configuration = configuration;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

            var basket = await _baskedRepository.GetBasketAsync(basketId);

            if (basket == null) {
                return null;
                    };
            var shippingPrice = 0m;
            if (basket.DelivertMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DelivertMethodId.Value);
                shippingPrice = deliveryMethod.Price;
            }
            foreach(var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if(item.Price != productItem.Price)
                {
                    item.Price= productItem.Price;
                }
            }

            var service = new PaymentIntentService();
            PaymentIntent intent;
            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + ((long)shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + ((long)shippingPrice * 100),
                    
                };

                await service.UpdateAsync(basket.PaymentIntentId,options);
            }
            await _baskedRepository.UpdateBasketAsync(basket);
            return basket;
        }

        public Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            throw new NotImplementedException();
        }
    }
}
