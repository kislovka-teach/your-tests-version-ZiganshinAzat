using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Abstractions;

public interface IUserRepository
{
    Task<User> Add(User user);
    
    Task<User?> GetByLogin(string login);
}