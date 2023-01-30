using System.Net;
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

  [Fact]
  public async Task Get_Products_Empty_Response()
  {
    var sut = MakeSut();

    var response = await sut.Client.GetAsync("api/Product");

    response.EnsureSuccessStatusCode();
    Assert.Equivalent(new List<Product>(), JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync()));
  }

  [Theory]
  [ClassData(typeof(GetProductsTestData))]
  public async Task Get_Product(List<Product> expectedProducts)
  {
    // Arrange
    var sut = MakeSut();
    sut.Context.Products.AddRange(expectedProducts);
    await sut.Context.SaveChangesAsync();

    // Act
    var response1 = await sut.Client.GetAsync("api/Product/1");
    var response2 = await sut.Client.GetAsync("api/Product/2");

    // Assert
    response1.EnsureSuccessStatusCode();
    response2.EnsureSuccessStatusCode();
    Assert.Equivalent(expectedProducts[0], JsonConvert.DeserializeObject<Product>(await response1.Content.ReadAsStringAsync()));
    Assert.Equivalent(expectedProducts[1], JsonConvert.DeserializeObject<Product>(await response2.Content.ReadAsStringAsync()));
  }

  [Fact]
  public async Task Post_Product()
  {
    var sut = MakeSut();
    var product = new Product
    {
      Id = 1,
      Name = "Name1",
      Description = "Description1",
      Price = 1.0m
    };
    var contentString = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

    var response = await sut.Client.PostAsync("api/Product", contentString);

    response.EnsureSuccessStatusCode();
    Assert.Equivalent(sut.Context.Products.Single((p => p.Id == 1)), product);
  }

  [Fact]
  public async Task Post_Product_Should_Respond_409_When_Product_Already_Exists()
  {
    var sut = MakeSut();
    var product = new Product
    {
      Id = 1,
      Name = "Name1",
      Description = "Description1",
      Price = 1.0m
    };
    sut.Context.Products.Add(product);
    await sut.Context.SaveChangesAsync();
    var contentString = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

    var response = await sut.Client.PostAsync("api/Product", contentString);

    Assert.Equal((HttpStatusCode)409, response.StatusCode);
  }

  [Fact]
  public async Task Put_Product()
  {
    var sut = MakeSut();
    var product = new Product
    {
      Id = 1,
      Name = "Name1",
      Description = "Description1",
      Price = 1.0m
    };
    sut.Context.Products.Add(product);
    await sut.Context.SaveChangesAsync();
    product.Name = "Name2";
    product.Description = "Description2";
    product.Price = 2m;
    var contentString = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

    var response = await sut.Client.PutAsync("api/Product/1", contentString);

    response.EnsureSuccessStatusCode();
    Assert.Equivalent(sut.Context.Products.Single((p => p.Id == 1)), product);
  }

  [Fact]
  public async Task Put_Product_Should_Respond_404_When_Product_Not_Found()
  {
    var sut = MakeSut();
    var product = new Product
    {
      Id = 1,
      Name = "Name1",
      Description = "Description1",
      Price = 1.0m
    };
    var contentString = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

    var response = await sut.Client.PutAsync("api/Product/1", contentString);

    Assert.Equal((HttpStatusCode)404, response.StatusCode);
  }

  [Fact]
  public async Task Put_Product_Should_Respond_400_When_Product_Id_Doesnt_Match()
  {
    var sut = MakeSut();
    var product = new Product
    {
      Id = 1,
      Name = "Name1",
      Description = "Description1",
      Price = 1.0m
    };
    sut.Context.Products.Add(product);
    await sut.Context.SaveChangesAsync();
    product.Id = 2;

    var contentString = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

    var response = await sut.Client.PutAsync("api/Product/1", contentString);

    Assert.Equal((HttpStatusCode)400, response.StatusCode);
  }

  [Fact]
  public async Task Delete_Product()
  {
    var sut = MakeSut();
    var product = new Product
    {
      Id = 1,
      Name = "Name1",
      Description = "Description1",
      Price = 1.0m
    };
    sut.Context.Products.Add(product);
    await sut.Context.SaveChangesAsync();

    var response = await sut.Client.DeleteAsync("api/Product/1");

    response.EnsureSuccessStatusCode();
    Assert.Equivalent(sut.Context.Products.Any((p => p.Id == 1)), false);
  }

  [Fact]
  public async Task Delete_Product_Should_Respond_404_When_Product_Not_Found()
  {
    var sut = MakeSut();

    var response = await sut.Client.DeleteAsync("api/Product/1");

    Assert.Equal((HttpStatusCode)404, response.StatusCode);
  }
}