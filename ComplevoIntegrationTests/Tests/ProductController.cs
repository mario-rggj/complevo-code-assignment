using Microsoft.AspNetCore.Mvc.Testing;

namespace ComplevoIntegrationTests.Tests;

public class BasicTests
  : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly WebApplicationFactory<Program> _factory;

  public BasicTests(WebApplicationFactory<Program> factory)
  {
    _factory = factory;
  }

  [Fact]
  public async Task Test()
  {
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.GetAsync("api/Product");

    // Assert
    response.EnsureSuccessStatusCode();
  }
}