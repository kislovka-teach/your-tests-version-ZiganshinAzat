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
        
        // Пароли: password1, password2, password3 соответственно
        modelBuilder.Entity<User>().HasData(
            new User { Name = "User1", Login = "user1", Password = "434F30F6A76EF55ECAE03197BEC4A3F14CA29DE9979E3A84D6B6A93857B6BAE4:FEFF4E79FB775715E64947DD1B5A4822:50000:SHA256", Role = UserRole.Admin },
            new User { Name = "User2", Login = "user2", Password = "FBAAD6BBE5DFCF68420FA28FB05BEB6B2BE0563D59314A18E2E440F4D25E2429:9BCB8CA1DB63344126C5A316DA1459C4:50000:SHA256", Role = UserRole.Manager },
            new User { Name = "User3", Login = "user3", Password = "4A7353495B9C92C235A2BAFFBBE368125953FF25AB6513B2D610D0AFAA437244:03170422FDDAB4ECA63D08A92A59D564:50000:SHA256", Role = UserRole.RegularUser }
        );
    }
}