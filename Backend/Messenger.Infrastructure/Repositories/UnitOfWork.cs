using System.Collections;
using Messenger.Application.Interfaces.Repositories;
using Messenger.Domain.Common;
using Messenger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Messenger.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly MessengerDbContext _context;
    private IDbContextTransaction? _currentTransaction;
    private Hashtable? _repositories; 
    private bool _disposed;
    
    public UnitOfWork(MessengerDbContext context) => _context = context;

    public IRepository<T, TKey> Repository<T, TKey>() where T : class, IEntity<TKey>
    {
        _repositories ??= new Hashtable();

        var type = typeof(T).Name;

        if (_repositories.ContainsKey(type)) return (IRepository<T, TKey>)_repositories[type]!;
        
        var repositoryType = typeof(Repository<,>);
        var repositoryInstance = Activator.CreateInstance(
            repositoryType.MakeGenericType(typeof(T), typeof(TKey)), 
            _context);

        _repositories.Add(type, repositoryInstance);

        return (IRepository<T, TKey>)_repositories[type]!;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default) 
        => await _context.SaveChangesAsync(ct);

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default)
    {
        _currentTransaction = await _context.Database.BeginTransactionAsync(ct);
        return _currentTransaction;
    }

    public async Task CommitAsync(CancellationToken ct = default)
    {
        try
        {
            await _context.SaveChangesAsync(ct);
            if (_currentTransaction != null) await _currentTransaction.CommitAsync(ct);
        }
        catch
        {
            await RollbackAsync(ct);
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackAsync(CancellationToken ct = default)
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.RollbackAsync(ct);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;

            _context.Dispose();
        }
        _disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            if (_currentTransaction != null) await _currentTransaction.DisposeAsync();
            await _context.DisposeAsync();
        }
    }
}
