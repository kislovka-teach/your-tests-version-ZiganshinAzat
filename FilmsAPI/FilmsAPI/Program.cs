using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FilmsAPI;
using FilmsAPI.Abstractions;
using FilmsAPI.Entities;
using FilmsAPI.Repositories;
using FilmsAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Username=postgres;Password=2114;Database=FilmAPI;Port=1473;"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IUserService, UserService>();

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

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("U5o#29$NHG!pA7@6QxZp6mFv*Se8Dz$L");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Results.Ok();
        }
        else
        {
            return Results.Unauthorized();
        }
    }
});

app.MapPost("/films/add", [Authorize(Roles = "Admin")] async (HttpContext context, IUnitOfWork unitOfWork, IFilmRepository filmRepository) =>
{
    var film = await context.Request.ReadFromJsonAsync<Film>();

    if (film == null)
    {
        return Results.BadRequest("Invalid project data");
    }

    var addedProject = await filmRepository.Add(film);
    await unitOfWork.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/films/{filmId}",[Authorize] async (Guid filmId, IFilmRepository filmRepository) =>
{
    var film = await filmRepository.GetFilmById(filmId);

    if (film == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(film);
});

app.MapGet("/films/year/{year}", [Authorize] async (int year, IFilmRepository filmRepository) =>
{
    var film = await filmRepository.GetFilmsByYear(year);

    return Results.Ok(film);
});

app.MapPost("/actors/add",[Authorize(Roles = "Admin")] async (HttpContext context, IUnitOfWork unitOfWork, IActorRepository actorRepository) =>
{
    var actor = await context.Request.ReadFromJsonAsync<Actor>();

    if (actor == null)
    {
        return Results.BadRequest("Invalid project data");
    }

    var addedActor = await actorRepository.Add(actor);
    await unitOfWork.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/actors/{actorId}",[Authorize] async (Guid actorId, IActorRepository actorRepository) =>
{
    var actor = await actorRepository.GetActorById(actorId);

    if (actor == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(actor);
});

app.MapPost("/companies/add",[Authorize(Roles = "Admin")] async (HttpContext context, IUnitOfWork unitOfWork, ICompanyRepository companyRepository) =>
{
    var company = await context.Request.ReadFromJsonAsync<Company>();

    if (company == null)
    {
        return Results.BadRequest("Invalid project data");
    }

    var addedCompany = await companyRepository.Add(company);
    await unitOfWork.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/companies/{companyId}",[Authorize] async (Guid companyId, ICompanyRepository companyRepository) =>
{
    var company = await companyRepository.GetCompanyById(companyId);

    if (company == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(company);
});

app.MapGet("/films/{filmId}/cast",[Authorize] async (Guid filmId, IFilmRepository filmRepository) =>
{
    var actors = await filmRepository.GetFilmActors(filmId);

    return Results.Ok(actors);
});

app.MapGet("/actors/{actorId}/films",[Authorize] async (Guid actorId, IActorRepository actorRepository) =>
{
    var films = await actorRepository.GetActorFilms(actorId);

    return Results.Ok(films);
});

app.Run();