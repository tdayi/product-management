using MassTransit;
using Microsoft.EntityFrameworkCore;
using PM.Core.Database.PostgreSql.DbSessionFactory;
using PM.Core.Database.PostgreSql.UnitOfWork;
using PM.Core.Database.UnitOfWork;
using PM.Domain.Category.Repository;
using PM.Domain.Product.Repository;
using PM.Domain.Repository.Concrete;
using PM.Domain.Repository.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

string rabbitMqHostName = config["RabbitMq:HostName"];
string rabbitMqUsername = config["RabbitMq:Username"];
string rabbitMqPassword = config["RabbitMq:Password"];

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        busFactoryConfigurator.Host(rabbitMqHostName, hostConfigurator =>
        {
            hostConfigurator.Username(rabbitMqUsername);
            hostConfigurator.Password(rabbitMqPassword);
        });
    });
});

builder.Services.AddScoped<IDbSessionFactory, PMDbContextFactory>();
builder.Services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

var optionsBuilder = new DbContextOptionsBuilder<PMDbContext>();
using (var context = new PMDbContext(optionsBuilder.Options))
{
    context.Database.Migrate();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();