using TaskManagerAPI.Abstractions;

namespace TaskManagerAPI.Repositories;

public class UnitOfWork(AppDbContext appDbContext):IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return appDbContext.SaveChangesAsync(cancellationToken);
    }
}