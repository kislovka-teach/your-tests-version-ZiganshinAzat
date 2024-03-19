using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (!Users.Any())
        {
            modelBuilder.Entity<User>().HasData(
                new User { Name = "User1", Login = "user1", Password = "password1", Role = UserRole.Admin },
                new User { Name = "User2", Login = "user2", Password = "password2", Role = UserRole.Manager },
                new User { Name = "User3", Login = "user3", Password = "password3", Role = UserRole.RegularUser }
            );
        }
    }
}