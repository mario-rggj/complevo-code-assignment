using System.Collections.ObjectModel;
using Complevo.Domain.Models;

namespace Complevo.Interface.Dtos.ProductDtos;

public class GetProductDto
{
  public GetProductDto(Product product)
  {
    Id = product.Id;
    Name = product.Name;
    Description = product.Description;
    Price = product.Price;
  }

  public int Id { get; set; }
  public string Name { get; set; }
  public string? Description { get; set; }
  public decimal Price { get; set; }

  public static Collection<GetProductDto> fromProducts(IEnumerable<Product> products)
  {
    var productsDto = new Collection<GetProductDto>();

    foreach (var product in products) productsDto.Add(new GetProductDto(product));

    return productsDto;
  }
}