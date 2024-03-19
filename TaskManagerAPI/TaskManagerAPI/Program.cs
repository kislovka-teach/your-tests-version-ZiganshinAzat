using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManagerAPI;
using TaskManagerAPI.Abstractions;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Repositories;
using TaskManagerAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Username=postgres;Password=2114;Database=TaskManagerAPI;Port=1473;"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthorization();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IIssueRepository, IssueRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization(); 

app.MapPost("/login", async (IUserService userService, HttpContext context) =>
{
    if (!context.Request.HasJsonContentType())
    {
        return Results.BadRequest("Unsupported Content-Type. Expected application/json.");
    }
    
    using (var reader = new StreamReader(context.Request.Body))
    {
        var userLoginModel = await context.Request.ReadFromJsonAsync<User>();

        var user = await userService.LoginUser(new User { Login = userLoginModel.Login, Password = userLoginModel.Password });

        if (user != null)
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
    }
});

app.MapPost("/projects/add", [Authorize(Roles = "Admin")] async (HttpContext context, IUnitOfWork unitOfWork, IProjectRepository projectRepository) =>
{
    var project = await context.Request.ReadFromJsonAsync<Project>();

    if (project == null)
    {
        return Results.BadRequest("Invalid project data");
    }

    var addedProject = await projectRepository.Add(project);
    await unitOfWork.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/projects/{projectId}/issues", [Authorize] async (Guid projectId, IIssueRepository issueRepository) =>
{
    var issues = await issueRepository.GetAllIssuesOfProject(projectId);
    
    return Results.Ok(issues);
});

app.MapGet("/projects/all",[Authorize] async (IProjectRepository projectRepository) =>
{
    var allProjects = await projectRepository.GetAllProjects();

    return Results.Ok(allProjects);
});

app.MapPost("/issues/add",[Authorize(Roles = "Admin, Manager")] async (HttpContext context, IUnitOfWork unitOfWork, IIssueRepository issueRepository) =>
{
    var issue = await context.Request.ReadFromJsonAsync<Issue>();

    if (issue == null)
    {
        return Results.BadRequest("Invalid project data");
    }

    var addedIssue = await issueRepository.Add(issue);
    await unitOfWork.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/issues/{issueId}/comments",[Authorize] async (Guid issueId, ICommentRepository commentRepository) =>
{
    var issues = await commentRepository.GetAllCommentsOfIssue(issueId);

    return Results.Ok(issues);
});

app.MapPost("/comments/add",[Authorize] async (HttpContext context, IUnitOfWork unitOfWork, ICommentRepository commentRepository) =>
{
    var comment = await context.Request.ReadFromJsonAsync<Comment>();

    if (comment == null)
    {
        return Results.BadRequest("Invalid project data");
    }

    var addedComment = await commentRepository.AddComment(comment);
    await unitOfWork.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/comments/{commentId}/delete",[Authorize(Roles = "Admin")] async (Guid commentId, ICommentRepository commentRepository) =>
{
    commentRepository.RemoveComment(commentId);

    return Results.Ok();
});

app.MapGet("/users/{userLogin}/issues",[Authorize] async (String userLogin, IIssueRepository issueRepository) =>
{
    var issues = await issueRepository.GetAllUserIssues(userLogin);

    return Results.Ok(issues);
});

app.MapGet("/users/{userLogin}/projects",[Authorize] async (String userLogin, IProjectRepository projectRepository) =>
{
    var projects = await projectRepository.GetAllProjectsByUser(userLogin);

    return Results.Ok(projects);
});

app.Run();