using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;
using Talabat.Core.IServices;
using Talabat.Dtos;
using Talabat.Error;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return BadRequest(
                    new ErrorResponse(HttpStatusCode.BadRequest, "Given Email or Password is not Correct"));

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if(!result)
                return BadRequest(
                    new ErrorResponse(HttpStatusCode.BadRequest, "Given Email or Password is not Correct"));

            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                Token = _authService.GenerateTokenAsync(user).Result
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if(await _userManager.FindByEmailAsync(model.Email) is not null)
                return BadRequest(
                    new ErrorResponse(HttpStatusCode.BadRequest, "Given Email already Exists"));

            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return BadRequest(
                    new ErrorResponse(HttpStatusCode.BadRequest, "Given UserName already Exists"));

            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user,model.Password);
            if(!result.Succeeded)
                return BadRequest(
                    new ErrorResponse(HttpStatusCode.BadRequest, "Password is not Correct"));
            
            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                Token = _authService.GenerateTokenAsync(user).Result
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? "";
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<IActionResult> GetUserAddress()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.Users.Include(u => u.Address).SingleOrDefaultAsync(u => u.Email == userEmail);
            
            return Ok(_mapper.Map<UserAddressDTO>(user?.Address));
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<IActionResult> UpdateAddress(UserAddressDTO address)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.Users.Include(u => u.Address).SingleOrDefaultAsync(u => u.Email == userEmail);

            if (user?.Address != null)
            {
                _mapper.Map(address, user.Address);
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest));

            return Ok(_mapper.Map<UserAddressDTO>(user?.Address));
        }
    }
}
