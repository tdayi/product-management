using PM.Core.Database.UnitOfWork;
using PM.Core.Entity;

namespace PM.Core.Database.Repository;

public interface IRepository<TEntity, Key> where TEntity : IEntity<Key>
{
    Task<TEntity> GetByKeyAsync(IUnitOfWork unitOfWork, Key key, CancellationToken cancellationToken = default);
    Task InsertAsync(IUnitOfWork unitOfWork, TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(IUnitOfWork unitOfWork, TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(IUnitOfWork unitOfWork, TEntity entity, CancellationToken cancellationToken = default);
}