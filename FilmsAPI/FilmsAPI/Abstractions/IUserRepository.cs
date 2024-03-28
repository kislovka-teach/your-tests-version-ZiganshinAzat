using FilmsAPI.Entities;

namespace FilmsAPI.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByLogin(string login);
}