using System.Data.Common;
using Complevo.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComplevoIntegrationTests;

public class CustomWebApplicationFactory<TProgram>
  : WebApplicationFactory<TProgram> where TProgram : class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices((context, services) =>
    {
      var dbContextDescriptor = services.SingleOrDefault(
        d => d.ServiceType ==
             typeof(DbContextOptions<ApiContext>));

      services.Remove(dbContextDescriptor);

      var dbConnectionDescriptor = services.SingleOrDefault(
        d => d.ServiceType ==
             typeof(DbConnection));

      services.Remove(dbConnectionDescriptor);

      // Create open SqliteConnection so EF won't automatically close it.
      services.AddSingleton<DbConnection>(container =>
      {
        var connection = new SqlConnection("DataSource=:memory:");
        connection.Open();

        return connection;
      });

      var connectionString = context.Configuration.GetConnectionString("Default")?
        .Replace("Database=Complevo;", "Database=ComplevoTest-" + Guid.NewGuid() + ";");

      services.AddDbContext<ApiContext>(options =>
        options.UseSqlServer(connectionString));
    });

    builder.UseEnvironment("Development");
  }
}