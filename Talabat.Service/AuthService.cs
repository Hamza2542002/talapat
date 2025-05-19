using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.IServices;

namespace Talabat.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            // Private Claims 
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, user.UserName??""),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email??"")
            }.Union(userClaims).ToList();

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT")["SecurityKey"] ?? "asljdjklsahdjkshddasjkhdksa"));

            var signInCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var secuirityToken = new JwtSecurityToken(
                signingCredentials: signInCredentials,
                issuer : _configuration.GetSection("JWT")["Issure"],
                audience : _configuration.GetSection("JWT")["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(int.Parse(_configuration.GetSection("JWT")["DurationInDays"] ?? "5"))
            );
            return new JwtSecurityTokenHandler().WriteToken(secuirityToken);
        }
    }
}
