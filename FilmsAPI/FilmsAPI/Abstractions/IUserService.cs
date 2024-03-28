using FilmsAPI.Entities;

namespace FilmsAPI.Abstractions;

public interface IUserService
{
    Task<User?> LoginUser(User user);
}