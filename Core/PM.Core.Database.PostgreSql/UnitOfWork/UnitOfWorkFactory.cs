using PM.Core.Database.PostgreSql.DbSessionFactory;
using PM.Core.Database.UnitOfWork;

namespace PM.Core.Database.PostgreSql.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbSessionFactory _dbSessionFactory;

    public UnitOfWorkFactory(IDbSessionFactory dbSessionFactory)
    {
        _dbSessionFactory = dbSessionFactory;
    }

    public IUnitOfWork Create()
    {
        return new UnitOfWork(_dbSessionFactory.GetDbSessionModel());
    }
}