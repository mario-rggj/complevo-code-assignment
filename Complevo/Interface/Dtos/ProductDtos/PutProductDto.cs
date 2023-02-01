using Complevo.Domain.Models;

namespace Complevo.Interface.Dtos.ProductDtos;

public class PutProductDto
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public decimal Price { get; set; }

  public Product toProduct()
  {
    return new Product
    {
      Id = this.Id,
      Name = this.Name,
      Description = this.Description,
      Price = this.Price
    };
  }
}