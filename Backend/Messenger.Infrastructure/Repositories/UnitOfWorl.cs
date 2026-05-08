using Messenger.Application.Interfaces.Repositories;
using Messenger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Messenger.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MessengerDbContext _context;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(MessengerDbContext context) => _context = context;

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        return _currentTransaction;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            if (_currentTransaction != null) await _currentTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null) return;
        
        await _currentTransaction.RollbackAsync(cancellationToken);
        _currentTransaction.Dispose();
        _currentTransaction = null;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        => await _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        _context.Dispose();
        _currentTransaction?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (_currentTransaction != null) await _currentTransaction.DisposeAsync();
        await _context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}