using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Abstractions;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Repositories;

public class UserRepository(AppDbContext appDbContext): IUserRepository
{
    public async Task<User> Add(User user)
    {
        var result = await appDbContext.Users.AddAsync(user);
        return result.Entity;
    }
    
    public Task<User?> GetByLogin(string login)
    {
        return appDbContext.Users.SingleOrDefaultAsync(user => user.Login == login);
    }
}