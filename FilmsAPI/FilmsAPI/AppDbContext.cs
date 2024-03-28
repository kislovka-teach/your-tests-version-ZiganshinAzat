using FilmsAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<Actor> Actors { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Пароли: password1, password3 соответственно
        modelBuilder.Entity<User>().HasData(
            new User { Login = "user1", Password = "434F30F6A76EF55ECAE03197BEC4A3F14CA29DE9979E3A84D6B6A93857B6BAE4:FEFF4E79FB775715E64947DD1B5A4822:50000:SHA256", Role = Role.Admin },
            new User { Login = "user3", Password = "4A7353495B9C92C235A2BAFFBBE368125953FF25AB6513B2D610D0AFAA437244:03170422FDDAB4ECA63D08A92A59D564:50000:SHA256", Role = Role.RegularUser }
        );
    }
}