using System.Data;

namespace PM.Core.Database.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    TSession DbSession<TSession>();

    Task BeginAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default);

    Task CommitAsync(CancellationToken cancellationToken = default);
}