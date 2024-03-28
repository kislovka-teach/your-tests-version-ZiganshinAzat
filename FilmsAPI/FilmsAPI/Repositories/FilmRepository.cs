using FilmsAPI.Abstractions;
using FilmsAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Repositories;

public class FilmRepository(AppDbContext appDbContext): IFilmRepository
{
    public async Task<Film?> GetFilmById(Guid filmId)
    {
        var film = await appDbContext.Films.FindAsync(filmId);

        return film;    
    }

    public async Task<List<Film>> GetFilmsByYear(int year)
    {
        var films = await appDbContext.Films
            .Where(f => f.ReleaseYear == year)
            .ToListAsync();

        return films;
    }

    public async Task<Film> Add(Film film)
    {
        var result = await appDbContext.Films.AddAsync(film);
        return result.Entity;    
    }

    public async Task<List<Actor>> GetFilmActors(Guid filmId)
    {
        var film = await GetFilmById(filmId);
    
        if (film != null)
        {
            return film.Actors;
        }
        else
        {
            return new List<Actor>();
        }    
    }
}