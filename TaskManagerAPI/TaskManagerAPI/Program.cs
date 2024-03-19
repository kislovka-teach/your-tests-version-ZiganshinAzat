using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI;
using TaskManagerAPI.Abstractions;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Repositories;
using TaskManagerAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database:ConnectionString")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
builder.Services.AddAuthorization();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IIssueRepository, IssueRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization(); 

app.MapPost("/login", async (IUserService userService, HttpContext context) =>
{
    var form = context.Request.Form;
    
    string login = form["login"];
    string password = form["password"];
    
    var user = await userService.LoginUser(new User { Login = login, Password = password });

    if (user is not null)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Role, user.Role.ToString()) };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return Results.Ok(); 
    }
    else
    {
        return Results.Unauthorized();
    }
});

app.Run();