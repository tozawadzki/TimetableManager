using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimetableManager.App.Helpers;
using TimetableManager.Helpers.Interfaces;
using TimetableManager.Helpers.Models;

namespace TimetableManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ITokenService _tokenService;

        public UsersController(IUsersService usersService, ITokenService tokenService)
        {
            _usersService = usersService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("new")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserCreateRequest newUserDto)
        {
            var result = await _usersService.CreateUser(newUserDto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var userDto = await _usersService.FindUserByEmail(newUserDto.Email);
            return CreatedAtAction(nameof(GetUser), userDto);
        }

        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetUser()
        {
            var userDto = await _usersService.FindUserById(User.GetId());
            return Ok(userDto);
        }

        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken(UserLoginRequest loginDataDto)
        {
            var token = await _tokenService.GetToken(loginDataDto);
            if (token == null)
            {
                return BadRequest();
            }

            return Ok(token);
        }

    }
}
