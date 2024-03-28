using FilmsAPI.Abstractions;
using FilmsAPI.Entities;

namespace FilmsAPI.Services;

public class UserService(IUserRepository userRepository,IPasswordHasherService passwordHasher): IUserService
{
    public async Task<User?> LoginUser(User inputUser)
    {
        var user = await userRepository.GetByLogin(inputUser.Login);
        if (user is null)
        {
            return null;
        }

        if (passwordHasher.Verify(inputUser.Password, user.Password))
        {
            return user;
        }

        return null;
    }
}