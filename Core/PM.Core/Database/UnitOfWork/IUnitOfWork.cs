using System.Data;

namespace PM.Core.Database.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    DbSession DbSession<DbSession>();

    Task BeginAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default);

    Task CommitAsync(CancellationToken cancellationToken = default);
}