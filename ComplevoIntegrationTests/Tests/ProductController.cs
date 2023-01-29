using System.Text;
using Complevo.Domain.Models;
using ComplevoIntegrationTests.Helpers;
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
    var sut = MakeSut();
    sut.Context.Products.AddRange(expectedProducts);
    await sut.Context.SaveChangesAsync();

    // Act
    var response = await sut.Client.GetAsync("api/Product");

    // Assert
    response.EnsureSuccessStatusCode();
    Assert.Equivalent(expectedProducts, JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync()));
  }
}