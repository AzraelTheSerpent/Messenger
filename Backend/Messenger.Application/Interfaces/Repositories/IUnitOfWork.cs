using Microsoft.EntityFrameworkCore.Storage;

namespace Messenger.Application.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}