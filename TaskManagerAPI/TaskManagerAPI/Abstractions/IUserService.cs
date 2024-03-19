using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Abstractions;

public interface IUserService
{
    Task<User?> LoginUser(User user);
}