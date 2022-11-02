using MassTransit;

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