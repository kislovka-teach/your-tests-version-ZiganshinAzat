using FilmsAPI.Entities;

namespace FilmsAPI.Abstractions;

public interface IFilmRepository
{
    Task<Film?> GetFilmById(Guid filmId);
    
    Task<List<Film>> GetFilmsByYear(int year);

    Task<Film> Add(Film film);

    Task<List<Actor>> GetFilmActors(Guid filmId);
}
