using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PM.Domain.Product.Command;

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
    });

await builder.RunConsoleAsync();