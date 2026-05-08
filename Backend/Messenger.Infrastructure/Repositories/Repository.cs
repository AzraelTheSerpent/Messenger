using Messenger.Application.Interfaces.Repositories;
using Messenger.Domain.Common;
using Messenger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastructure.Repositories;

public class Repository<T, TKey> : IRepository<T, TKey> where T : class, IEntity<TKey>
{
    private readonly MessengerDbContext _context;
    private DbSet<T> Set => _context.Set<T>();

    public Repository(MessengerDbContext context) => _context = context;
        
    public async Task AddAsync(T entity, CancellationToken cancellationToken) 
        => await Set.AddAsync(entity, cancellationToken);

    public void Delete(T entity) => Set.Remove(entity);

    public async Task DeleteByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        if (entity == null)
            throw new KeyNotFoundException($"Entity by Id {id} was not found!");

        Delete(entity);
    }

    public async Task<T?> GetByIdAsync(TKey id, CancellationToken cancellationToken) 
        => await Set.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);

    public IQueryable<T> GetQuery() => Set;
}