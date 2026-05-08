using Messenger.Domain.Common;

namespace Messenger.Application.Interfaces.Repositories;

public interface IRepository<T, in TKey> where T : class, IEntity<TKey>
{
    public Task AddAsync(T entity, CancellationToken cancellationToken);
    public void Delete(T entity);
    public Task<T?> GetByIdAsync(TKey id, CancellationToken cancellationToken);
    public Task DeleteByIdAsync(TKey id, CancellationToken cancellationToken);
    public IQueryable<T> GetQuery();
}