using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Dtos;
using Talabat.Error;
using Talabat.Helpers;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartsController(ICartRepository cartRepository , IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCart(string id)
        {
            var cart = await _cartRepository.GetCustomerCartAsync(id);
            if (cart is null)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest
                    , "There is no Cart with this id"));
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(CustomerCartDTO cart)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newCart = await _cartRepository
                .UpdateCustomerCartAsync(_mapper.Map<CustomerCartDTO,CustomerCart>(cart));

            if (newCart is null)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest
                    , "Coudn't Add the Cart, Please Try again"));

            return Ok(newCart);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCart(CustomerCartDTO cart)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newCart = await _cartRepository
                .UpdateCustomerCartAsync(_mapper.Map<CustomerCartDTO, CustomerCart>(cart));

            if (newCart is null)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest
                    , "Coudn't Update the Cart, Please Try again"));

            return Ok(newCart);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(string id)
        {
            var result = await _cartRepository.DeleteCustomerCartAsync(id);
            if (!result)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest
                    , "Coudn't Delete the Cart, Please Try again"));
            return Ok(new BaseResponse(HttpStatusCode.NoContent,"Deleted"));
        }
    }
}
