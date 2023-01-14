using AutoMapper;
using Ecom.BLL.Entities;
using Ecom.BLL.Entities.Order;
using Ecom.BLL.Interfaces;
using ecommerce.Dto;
using ecommerce.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;

namespace ecommerce.Controllers
{

    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Ecom.BLL.Entities.Order.Order>> CreateOrder(OrderDto orderDto)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var address = _mapper.Map<Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
            if (order is null)
                return BadRequest(new ApiResponse(400, "Problem Creating Order"));
            return Ok(order);
        }
        [HttpGet("GetOrdersForUser")]
        public async Task<ActionResult<IReadOnlyList<OrderDetailsDto>>> GetOrdersForUser()
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersForUserAsync(email);


            return Ok(_mapper.Map<IReadOnlyList<OrderDetailsDto>>(orders));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsDto>> GetOrderByIdForUser(int id)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdAsync(id, email);
            if (order is null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<OrderDetailsDto>(order));
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
            => Ok(await _orderService.GetDeliveryMethodsAsync());


    }
}
