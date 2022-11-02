using Microsoft.EntityFrameworkCore;
using PM.Core.Database.Repository;
using PM.Core.Database.UnitOfWork;
using PM.Core.Entity;

namespace PM.Core.Database.PostgreSql.Repository;

public class Repository<TEntity, Key> : IRepository<TEntity, Key> where TEntity : class, IEntity<Key>
{
    public async Task<TEntity> GetByKeyAsync(IUnitOfWork unitOfWork, Key key,
        CancellationToken cancellationToken = default)
    {
        var dbContext = unitOfWork.DbSession<DbContext>();
        var dbSet = dbContext.Set<TEntity>();

        return await dbSet.Where(x => x.Id.Equals(key)).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task InsertAsync(IUnitOfWork unitOfWork, TEntity entity, CancellationToken cancellationToken = default)
    {
        var dbContext = unitOfWork.DbSession<DbContext>();
        var dbSet = dbContext.Set<TEntity>();

        await dbSet.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(IUnitOfWork unitOfWork, TEntity entity, CancellationToken cancellationToken = default)
    {
        var dbContext = unitOfWork.DbSession<DbContext>();
        var dbSet = dbContext.Set<TEntity>();

        dbSet.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(IUnitOfWork unitOfWork, TEntity entity, CancellationToken cancellationToken = default)
    {
        var dbContext = unitOfWork.DbSession<DbContext>();
        var dbSet = dbContext.Set<TEntity>();

        dbSet.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}