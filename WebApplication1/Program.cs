using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.UseCases;
using WebApplication1.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("IS_RUNNING_ON_DOCKER") == "true" ? "Docker" : "Default";

// Add services to the container.
builder.Services.AddDbContext<ApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString)));
builder.Services.AddScoped<GetProductsUseCase>();
builder.Services.AddScoped<GetProductUseCase>();
builder.Services.AddScoped<PutProductUseCase>();
builder.Services.AddScoped<PostProductUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();
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