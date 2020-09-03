using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TimetableManager.Helpers.Interfaces;
using TimetableManager.Helpers.Models;
using TimetableManager.Repositories.Models;

namespace TimetableManager.Helpers.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public TokenService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> GetToken(UserLoginRequest loginDataDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDataDto.Email);
            if (user == null)
            {
                return null;
            }

            bool passwordCorrect = await _userManager.CheckPasswordAsync(user, loginDataDto.Password);
            if (!passwordCorrect)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtToken:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(
                    _configuration.GetValue<int>("JwtToken:ExpirationMinutes")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateEncodedJwt(tokenDescriptor);

            return token;
        }
    }
}
