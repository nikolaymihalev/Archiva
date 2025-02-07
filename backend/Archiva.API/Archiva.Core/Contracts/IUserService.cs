using Archiva.Core.Models;
using Archiva.Core.Models.User;

namespace Archiva.Core.Conracts;

public interface IUserService
{
    Task LoginAsync(LoginModel model);
    Task RegisterAsync(RegisterModel model);
    Task<UserModel> GetUserByIdAsync(string id);
    Task<UserModel> GetUserByEmailAsync(string email);
}
