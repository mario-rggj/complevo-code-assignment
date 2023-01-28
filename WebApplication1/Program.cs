using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.UseCases;
using WebApplication1.Domain.Infrastructure;
using WebApplication1.Domain.Infrastructure.Repositories;
using WebApplication1.Domain.UseCases;
using WebApplication1.Infrastructure;
using WebApplication1.Infrastructure.Context;
using WebApplication1.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("IS_RUNNING_ON_DOCKER") == "true" ? "Docker" : "Default";

// Add services to the container.
builder.Services.AddDbContext<ApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString)));
builder.Services.AddScoped<IGetProductsUseCase, GetProductsUseCase>();
builder.Services.AddScoped<IGetProductUseCase, GetProductUseCase>();
builder.Services.AddScoped<IPutProductUseCase, PutProductUseCase>();
builder.Services.AddScoped<IPostProductUseCase,PostProductUseCase>();
builder.Services.AddScoped<IDeleteProductUseCase,DeleteProductUseCase>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    if (Environment.GetEnvironmentVariable("ENABLE_AUTOMATIC_MIGRATION") == "true")
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ApiContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();