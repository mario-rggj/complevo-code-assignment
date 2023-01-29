using Complevo.Domain.Models;
using Complevo.Infrastructure.Context;
using ComplevoIntegrationTests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ComplevoIntegrationTests.Tests;

public class ProductController
  : TestClass
{
  private readonly CustomWebApplicationFactory<Program> _factory;

  public ProductController(CustomWebApplicationFactory<Program> factory) : base(factory)
  {
    _factory = factory;
  }

  private class GetProductsTestData : TheoryData<List<Product>>
  {
    public GetProductsTestData()
    {
      Add(new List<Product>
      {
        new Product {Id = 1, Name = "Name1", Description = "Description1", Price = 1m},
        new Product {Id = 2, Name = "Name2", Description = "Description2", Price = 2m}
      });
    }
  }

  [Theory]
  [ClassData(typeof(GetProductsTestData))]
  public async Task Get_Products(List<Product> expectedProducts)
  {
    // Arrange
    var client = _factory.CreateClient();
    var db = GetDbContext();
    db.Products.AddRange(expectedProducts);
    await db.SaveChangesAsync();

    // Act
    var response = await client.GetAsync("api/Product");

    // Assert
    response.EnsureSuccessStatusCode();
    Assert.Equivalent(expectedProducts, JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync()));
  }
}