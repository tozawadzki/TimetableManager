using System.Threading.Tasks;
using TimetableManager.Helpers.Models;

namespace TimetableManager.Helpers.Interfaces
{
    public interface ITokenService
    {
        Task<string> GetToken(UserLoginRequest loginDataDto);
    }
}
