using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PM.Core.Database.PostgreSql.DbSessionFactory;

namespace PM.Domain.Repository.Context;

public class PMDbContextFactory : IDesignTimeDbContextFactory<PMDbContext>, IDbSessionFactory
{
    public PMDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PMDbContext>();
        optionsBuilder.UseNpgsql();

        return new PMDbContext(optionsBuilder.Options);
    }

    public DbSessionModel GetDbSessionModel()
    {
        var optionsBuilder = new DbContextOptionsBuilder<PMDbContext>();

        return new DbSessionModel()
        {
            DbSession = new PMDbContext(optionsBuilder.Options)
        };
    }
}