using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PM.Core.Database.PostgreSql.DbSessionFactory;
using PM.Core.Database.UnitOfWork;

namespace PM.Core.Database.PostgreSql.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private DbContext _dbContext;
    private IDbContextTransaction _dbContextTransaction;
    private readonly DbSessionModel _dbSessionModel;

    public UnitOfWork(DbSessionModel dbSessionModel)
    {
        _dbSessionModel = dbSessionModel;
    }

    public DbContext DbSession<DbContext>()
    {
        return (DbContext) _dbSessionModel.DbSession;
    }

    public async Task BeginAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default)
    {
        _dbContextTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
        _dbContextTransaction?.CommitAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        await _dbContextTransaction.DisposeAsync();
    }
}