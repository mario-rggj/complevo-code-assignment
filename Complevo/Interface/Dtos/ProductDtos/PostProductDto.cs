using Complevo.Domain.Models;

namespace Complevo.Interface.Dtos.ProductDtos;

public class PostProductDto
{
  public string Name { get; set; }
  public string Description { get; set; }
  public decimal Price { get; set; }

  public Product ToProduct()
  {
    return new Product
    {
      Name = Name,
      Description = Description,
      Price = Price
    };
  }
}