using FilmsAPI.Abstractions;
using FilmsAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Repositories;

public class UserRepository(AppDbContext appDbContext): IUserRepository
{
    public Task<User?> GetByLogin(string login)
    {
        return appDbContext.Users.SingleOrDefaultAsync(user => user.Login == login);
    }
}