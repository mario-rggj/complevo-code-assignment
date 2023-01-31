using Complevo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Complevo.Infrastructure.Context;

public class ProductConfiguration
{
  public ProductConfiguration(EntityTypeBuilder<Product> entityBuilder)
  {
    entityBuilder
      .Property(p => p.Price)
      .HasColumnType("decimal(18,2)");
  }
}