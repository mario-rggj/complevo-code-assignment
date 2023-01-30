using System.Net;
using System.Text;
using Complevo.Domain.Models;
using ComplevoIntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
        new Product {Name = "Name1", Description = "Description1", Price = 1m},
        new Product {Name = "Name2", Description = "Description2", Price = 2m},
        new Product {Name = "Name3", Description = "Description3", Price = 2m},
        new Product {Name = "Name4", Description = "Description4", Price = 2m},
        new Product {Name = "Name5", Description = "Description5", Price = 2m},
        new Product {Name = "Name6", Description = "Description6", Price = 2m},
        new Product {Name = "Name7", Description = "Description7", Price = 2m},
        new Product {Name = "Name8", Description = "Description8", Price = 2m},
        new Product {Name = "Name9", Description = "Description9", Price = 2m},
        new Product {Name = "Name10", Description = "Description10", Price = 2m},
        new Product {Name = "Name11", Description = "Description11", Price = 2m},
        new Product {Name = "Name12", Description = "Description12", Price = 2m},
        new Product {Name = "Name13", Description = "Description13", Price = 2m},
        new Product {Name = "Name14", Description = "Description14", Price = 2m},
        new Product {Name = "Name15", Description = "Description15", Price = 2m},
        new Product {Name = "Name16", Description = "Description16", Price = 2m},
        new Product {Name = "Name17", Description = "Description17", Price = 2m},
        new Product {Name = "Name18", Description = "Description18", Price = 2m},
        new Product {Name = "Name19", Description = "Description19", Price = 2m},
        new Product {Name = "Name20", Description = "Description20", Price = 2m}
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
    var response = await sut.Client.GetAsync("api/Product?pageSize=20");

    // Assert
    response.EnsureSuccessStatusCode();
    Assert.Equivalent(expectedProducts, JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync()));
  }

  [Theory]
  [ClassData(typeof(GetProductsTestData))]
  public async Task Get_Products_With_Pagination(List<Product> expectedProducts)
  {
    var sut = MakeSut();
    sut.Context.Products.AddRange(expectedProducts);
    await sut.Context.SaveChangesAsync();

    var page1DefaultSizeResponse = await sut.Client.GetAsync("api/Product");
    var page2DefaultSizeResponse = await sut.Client.GetAsync("api/Product?page=2");
    var page1Size10Response = await sut.Client.GetAsync("api/Product?page=1&pageSize=10");
    var page2Size10Response = await sut.Client.GetAsync("api/Product?page=2&pageSize=10");

    page1DefaultSizeResponse.EnsureSuccessStatusCode();
    expectedProducts.GetRange(0, 5).Should().BeEquivalentTo(JsonConvert.DeserializeObject<List<Product>>(await page1DefaultSizeResponse.Content.ReadAsStringAsync()));
    page2DefaultSizeResponse.EnsureSuccessStatusCode();
    expectedProducts.GetRange(5, 5).Should().BeEquivalentTo(JsonConvert.DeserializeObject<List<Product>>(await page2DefaultSizeResponse.Content.ReadAsStringAsync()));
    page1Size10Response.EnsureSuccessStatusCode();
    expectedProducts.GetRange(0, 10).Should().BeEquivalentTo(JsonConvert.DeserializeObject<List<Product>>(await page1Size10Response.Content.ReadAsStringAsync()));
    page2Size10Response.EnsureSuccessStatusCode();
    expectedProducts.GetRange(10, 10).Should().BeEquivalentTo(JsonConvert.DeserializeObject<List<Product>>(await page2Size10Response.Content.ReadAsStringAsync()));
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
    var response3 = await sut.Client.GetAsync("api/Product/21");

    // Assert
    response1.EnsureSuccessStatusCode();
    Assert.Equivalent(expectedProducts[0], JsonConvert.DeserializeObject<Product>(await response1.Content.ReadAsStringAsync()));
    response2.EnsureSuccessStatusCode();
    Assert.Equivalent(expectedProducts[1], JsonConvert.DeserializeObject<Product>(await response2.Content.ReadAsStringAsync()));
    Assert.Equal(HttpStatusCode.NotFound, response3.StatusCode);
  }

  [Fact]
  public async Task Post_Product()
  {
    var sut = MakeSut();
    var product = new Product
    {
      Name = "Name1",
      Description = "Description1",
      Price = 1.0m
    };
    var contentString = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

    var response = await sut.Client.PostAsync("api/Product", contentString);

    product.Id = 1;
    response.EnsureSuccessStatusCode();
    Assert.Equivalent(sut.Context.Products.Single((p => p.Id == 1)), product);
  }

  [Fact]
  public async Task Post_Product_Should_Respond_409_When_Product_Name_Already_Persisted()
  {
    var sut = MakeSut();
    var product = new Product
    {
      Name = "Name1",
      Description = "Description1",
      Price = 1.0m
    };
    sut.Context.Products.Add(product);
    await sut.Context.SaveChangesAsync();
    var contentString = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

    var response = await sut.Client.PostAsync("api/Product", contentString);

    product.Id = 1;
    Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    Assert.Equivalent(sut.Context.Products.Where(p => p.Id == 1).ToList().Count, 1);
  }

  [Fact]
  public async Task Post_Product_Should_Ignore_Id_Property()
  {
    var sut = MakeSut();
    var product = new Product
    {
      Name = "Name1",
      Description = "Description1",
      Price = 1.0m
    };
    sut.Context.Products.Add(product);
    await sut.Context.SaveChangesAsync();
    product.Name = "Name2";
    var contentString = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

    var response = await sut.Client.PostAsync("api/Product", contentString);

    Assert.Equal((HttpStatusCode)201, response.StatusCode);
  }

  [Fact]
  public async Task Put_Product()
  {
    var sut = MakeSut();
    var product = new Product
    {
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