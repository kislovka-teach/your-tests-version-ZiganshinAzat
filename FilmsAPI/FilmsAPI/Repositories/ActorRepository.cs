using FilmsAPI.Abstractions;
using FilmsAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Repositories;

public class ActorRepository(AppDbContext appDbContext): IActorRepository
{
    public async Task<Actor> Add(Actor actor)
    {
        var result = await appDbContext.Actors.AddAsync(actor);
        return result.Entity;    }

    public async Task<Actor?> GetActorById(Guid actorId)
    {
        var actor = await appDbContext.Actors.FindAsync(actorId);

        return actor;    
    }

    public async Task<List<Film>> GetActorFilms(Guid actorId)
    {
        var actor = await GetActorById(actorId);
    
        if (actor != null)
        {
            return actor.Films;
        }
        else
        {
            return new List<Film>();
        }
    }
}