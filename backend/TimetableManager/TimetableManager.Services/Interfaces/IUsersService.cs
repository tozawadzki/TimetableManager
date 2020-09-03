using System.Threading.Tasks;
using TimetableManager.Helpers.Models;
using Microsoft.AspNetCore.Identity;

namespace TimetableManager.Helpers.Interfaces
{
    public interface IUsersService
    {
        Task<UserInfoResponse> AuthenticateUser(UserLoginRequest loginDataDto);
        Task<IdentityResult> CreateUser(UserCreateRequest newUserDto);
        Task<UserInfoResponse> FindUserByEmail(string email);
        Task<UserInfoResponse> FindUserById(string id);
    }
}
