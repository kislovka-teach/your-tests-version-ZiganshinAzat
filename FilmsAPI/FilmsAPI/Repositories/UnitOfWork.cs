using FilmsAPI.Abstractions;

namespace FilmsAPI.Repositories;

public class UnitOfWork(AppDbContext appDbContext): IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return appDbContext.SaveChangesAsync(cancellationToken);
    }
}