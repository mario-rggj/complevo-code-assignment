using Complevo.Application.UseCases;
using Complevo.Domain.Infrastructure;
using Complevo.Domain.Infrastructure.Repositories;
using Complevo.Domain.UseCases;
using Complevo.Infrastructure;
using Complevo.Infrastructure.Context;
using Complevo.Infrastructure.Repositories;
using Complevo.Interface.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("IS_RUNNING_ON_DOCKER") == "true" ? "Docker" : "Default";
builder.Services.AddDbContext<ApiContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString)));

// Add services to the container.
builder.Services.AddScoped<IGetProductsUseCase, GetProductsUseCase>();
builder.Services.AddScoped<IGetProductUseCase, GetProductUseCase>();
builder.Services.AddScoped<IPutProductUseCase, PutProductUseCase>();
builder.Services.AddScoped<IPostProductUseCase, PostProductUseCase>();
builder.Services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
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

  using var scope = app.Services.CreateScope();
  var services = scope.ServiceProvider;

  var context = services.GetRequiredService<ApiContext>();
  if (context.Database.GetPendingMigrations().Any()) context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();

public partial class Program
{
}