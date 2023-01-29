using Complevo.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace ComplevoIntegrationTests.Helpers;

public class TestClass
  : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly CustomWebApplicationFactory<Program> _factory;

  protected TestClass(CustomWebApplicationFactory<Program> factory)
  {
    _factory = factory;
  }

  protected ApiContext GetDbContext()
  {
    var scope = _factory.Services.CreateScope();
    var scopedServices = scope.ServiceProvider;
    var db = scopedServices.GetRequiredService<ApiContext>();
    return db;
  }
}