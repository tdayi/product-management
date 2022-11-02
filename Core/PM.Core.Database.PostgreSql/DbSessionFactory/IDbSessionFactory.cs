namespace PM.Core.Database.PostgreSql.DbSessionFactory;

public interface IDbSessionFactory
{
    DbSessionModel GetDbSessionModel();
}