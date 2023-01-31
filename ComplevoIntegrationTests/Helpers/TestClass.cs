using Complevo.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace ComplevoIntegrationTests.Helpers;

public struct Sut
{
  public HttpClient Client;
  public ApiContext Context;
}

public class TestClass
  : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
  private readonly ApiContext _context;
  private readonly CustomWebApplicationFactory<Program> _factory;

  protected TestClass(CustomWebApplicationFactory<Program> factory)
  {
    _factory = factory;
    var scope = _factory.Services.CreateScope();
    var scopedServices = scope.ServiceProvider;
    var context = scopedServices.GetRequiredService<ApiContext>();
    _context = context;
    _context.Database.EnsureDeleted();
  }

  public void Dispose()
  {
    _context.Database.EnsureDeleted();
    _context.Dispose();
  }

  protected Sut MakeSut()
  {
    _context.Database.EnsureDeleted();
    _context.Database.EnsureCreated();
    return new Sut
    {
      Client = _factory.CreateClient(),
      Context = _context
    };
  }
}