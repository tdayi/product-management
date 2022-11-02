using PM.Core.Database.PostgreSql.DbSessionFactory;
using PM.Core.Database.PostgreSql.UnitOfWork;
using PM.Core.Database.UnitOfWork;
using PM.Domain.Category.Query;
using PM.Domain.Category.Repository;
using PM.Domain.Product.Product;
using PM.Domain.Product.Repository;
using PM.Domain.Repository.Concrete;
using PM.Domain.Repository.Context;
using PM.Query.Service.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbSessionFactory, PMDbContextFactory>();
builder.Services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<CategoryReader>();
builder.Services.AddScoped<ProductReader>();

builder.Services.AddAutoMapper(typeof(CategoryMapper));
builder.Services.AddAutoMapper(typeof(ProductMapper));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();