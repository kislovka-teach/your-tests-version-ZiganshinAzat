using FilmsAPI.Entities;

namespace FilmsAPI.Abstractions;

public interface IActorRepository
{
    Task<Actor> Add(Actor actor);

    Task<Actor?> GetActorById(Guid actorId);

    Task<List<Film>> GetActorFilms(Guid actorId);
}