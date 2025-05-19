using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Net;
using System.Security.Claims;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IServices;
using Talabat.Dtos;
using Talabat.Error;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentService paymentService,IOrderService orderService,IMapper mapper)
        {
            _paymentService = paymentService;
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreatePaymentInent(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _paymentService.CreateOrUpdatePaymentIntent(id,userEmail);

            if (order is null)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest));
            var newOrder = _mapper.Map<OrderToReturnDTO>(order);
            return Ok(newOrder);
        }

        [AllowAnonymous]
        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ParseEvent(json);
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentSucceeded:
                    await _orderService.UpdateOrderState(paymentIntent?.Id ?? "",OrderStatus.PayemntSucceded);
                    break;

                case EventTypes.PaymentIntentPaymentFailed:
                    await _orderService.UpdateOrderState(paymentIntent?.Id ?? "", OrderStatus.PayemntFailed);
                    break;

                default:
                    break;
            }
            
            return Ok();
        }
    }
}
