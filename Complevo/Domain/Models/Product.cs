namespace Complevo.Domain.Models;

public class Product : BaseModel
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string? Description { get; set; }
  public decimal Price { get; set; }
}