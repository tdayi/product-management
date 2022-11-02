using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PM.Domain.Repository.Context;

public class PMDbContext : DbContext
{
    public PMDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Domain.Category.Model.Category> Category { get; set; }
    public DbSet<Domain.Product.Model.Product> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(Domain.Repository.Mapping.Category)));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string appSettings = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        appSettings = appSettings ?? "Development";

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile($"appsettings.{appSettings}.json")
            .Build();

        string cnString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(cnString);
    }
}