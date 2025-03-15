using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Talabat.Core.Entities;
using Talabat.Core.IServices;
using Talabat.Error;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreatePaymentInent(string id)
        {
            var newCart = await _paymentService.CreateOrUpdatePaymentIntent(id);

            if (newCart is null)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest));

            return Ok(newCart);
        }
    }
}
