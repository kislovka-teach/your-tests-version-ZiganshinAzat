using FilmsAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Username=postgres;Password=2114;Database=FilmAPI;Port=1473;"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();