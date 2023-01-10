using AutoMapper;
using Ecom.BLL.Entities;
using Ecom.BLL.Interfaces;
using ecommerce.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IBaskedRepository _baskedRepository;
        private readonly IMapper _mapper;

        public BasketController(IBaskedRepository baskedRepository,IMapper mapper)
        {
            _baskedRepository = baskedRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _baskedRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UodateBasket(CustomerBasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basket);
            var updatedBasket = await _baskedRepository.UpdateBasketAsync(customerBasket);
            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasketById(string id)
        {
            await _baskedRepository.DeleteBasketAsync(id);
        }
    }
}
