using Archiva.Core.Conracts;
using Archiva.Core.Constants;
using Archiva.Core.Models;
using Archiva.Infrastructure.Common;
using Archiva.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Archiva.Core.Services;

public class UserService : IUserService
{
    private readonly IRepository repository;

    public UserService(IRepository _repository)
    {
        repository = _repository;
    }

    public async Task<UserModel> GetUserByEmailAsync(string email)
    {
        var user = await repository.AllReadonly<User>().FirstOrDefaultAsync(x => x.Email == email);

        if (user is null)
            throw new ArgumentException(string.Format(ReturnMessages.DoesntExist, "User"));

        return new UserModel()
        {
            Id = user.Id,
            Email = email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
        };
    }

    public async Task<UserModel> GetUserByIdAsync(string id)
    {
        var user = await repository.GetByIdAsync<User>(id);

        if (user is null)
            throw new ArgumentException(string.Format(ReturnMessages.DoesntExist, "User"));

        return new UserModel()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
        };
    }

    public async Task LoginAsync(LoginModel model)
    {
        var user = await repository.AllReadonly<User>()
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (user == null)
            throw new ArgumentException(string.Format(ReturnMessages.DoesntExist, "User"));

        var passwordHasher = new PasswordHasher<User>();

        var result = passwordHasher.VerifyHashedPassword(user!, user!.Password, model.Password);

        if (result != PasswordVerificationResult.Success)
            throw new ArgumentException(ReturnMessages.InvalidPassword);
    }

    public async Task RegisterAsync(RegisterModel model)
    {
        var user = await repository.AllReadonly<User>()
                .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (user != null)
            throw new ArgumentException(string.Format(ReturnMessages.AlreadyExist, "User"));

        user = new User()
        {
            Id = Guid.NewGuid().ToString(),
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        var passwordHasher = new PasswordHasher<User>();

        user.Password = passwordHasher.HashPassword(user, model.Password);

        await repository.AddAsync(user);
        await repository.SaveChangesAsync();
    }
}
