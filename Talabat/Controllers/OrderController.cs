using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IServices;
using Talabat.Core.Specifications.OrderSpecs;
using Talabat.Dtos;
using Talabat.Error;

namespace Talabat.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO order)
        {
            var CustomerEmail = User.FindFirstValue(ClaimTypes.Email);
            var createdOrder = await _orderService
                .CreateOrderAsync(CustomerEmail ?? "",
                                  order.CartId ?? "",
                                  order.DelevryMethodId,
                                  _mapper.Map<Address>(order.OrderAddress) ?? new());
            if (createdOrder is null)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest,"Couldn't Create The Order"));

            return Ok(createdOrder);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersForUser([FromQuery]OrderSpecsParams specsParams)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _orderService.GetOrdersForUserAsync(email ?? "", specsParams);
            if (result is null)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest));
            var orders = _mapper.Map<IReadOnlyList<OrderToReturnDTO>>(result);
            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderForUser(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await _orderService.GetOrderForUserAsync(id, userEmail ?? "");
            if(result is null ) return NotFound(new ErrorResponse(HttpStatusCode.NotFound));
            var order = _mapper.Map<OrderToReturnDTO>(result);
            return Ok(order);
        }

        [HttpGet("delivery-methods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var result = await _orderService.GetDeleveryMethods();
            return Ok(result);
        }
    }
}
