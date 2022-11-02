using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PM.Core.Database.PostgreSql.DbSessionFactory;
using PM.Core.Database.PostgreSql.UnitOfWork;
using PM.Core.Database.UnitOfWork;
using PM.Domain.Category.Repository;
using PM.Domain.Product.Command;
using PM.Domain.Product.Repository;
using PM.Domain.Repository.Concrete;
using PM.Domain.Repository.Context;

IConfigurationRoot configuration = default;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var builder = new HostBuilder().ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        configuration = config.Build();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IConfiguration>(configuration);

        string rabbitMqHostName = configuration["RabbitMq:HostName"];
        string rabbitMqUsername = configuration["RabbitMq:Username"];
        string rabbitMqPassword = configuration["RabbitMq:Password"];

        services.AddMassTransit(busConfigurator =>
        {
            var entryAssembly = Assembly.GetAssembly(typeof(ProductSaveCommand));

            busConfigurator.AddConsumers(entryAssembly);

            busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
            {
                busFactoryConfigurator.Host(rabbitMqHostName, hostConfigurator =>
                {
                    hostConfigurator.Username(rabbitMqUsername);
                    hostConfigurator.Password(rabbitMqPassword);
                });
                busFactoryConfigurator.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IDbSessionFactory, PMDbContextFactory>();
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    });

await builder.RunConsoleAsync();