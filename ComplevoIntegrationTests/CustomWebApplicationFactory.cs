using System.Data.Common;
using Complevo.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ComplevoIntegrationTests;

public class CustomWebApplicationFactory<TProgram>
  : WebApplicationFactory<TProgram> where TProgram : class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      var dbContextDescriptor = services.SingleOrDefault(
        d => d.ServiceType ==
             typeof(DbContextOptions<ApiContext>));

      services.Remove(dbContextDescriptor);

      var dbConnectionDescriptor = services.SingleOrDefault(
        d => d.ServiceType ==
             typeof(DbConnection));

      services.Remove(dbConnectionDescriptor);

      services.AddDbContext<ApiContext>(options =>
        options.UseInMemoryDatabase("ComplevoTest"));
    });

    builder.UseEnvironment("Development");
  }
}