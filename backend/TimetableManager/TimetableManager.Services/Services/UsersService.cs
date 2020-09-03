using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TimetableManager.Helpers.Interfaces;
using TimetableManager.Helpers.Models;
using TimetableManager.Repositories.Models;

namespace TimetableManager.Helpers.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UsersService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;   
        }

        public async Task<UserInfoResponse> AuthenticateUser(UserLoginRequest loginDataDto)
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

            var userDto = _mapper.Map<User, UserInfoResponse>(user);
            return userDto;
        }

        public async Task<IdentityResult> CreateUser(UserCreateRequest newUserDto)
        {
            var user = _mapper.Map<UserCreateRequest, User>(newUserDto);
            var result = await _userManager.CreateAsync(user, newUserDto.Password);
            return result;
        }

        public async Task<UserInfoResponse> FindUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userDto = _mapper.Map<User, UserInfoResponse>(user);
            return userDto;
        }

        public async Task<UserInfoResponse> FindUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userDto = _mapper.Map<User, UserInfoResponse>(user);
            return userDto;
        }
    }
}
