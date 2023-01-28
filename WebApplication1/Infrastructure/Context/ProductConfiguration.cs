using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Domain.Models;

namespace WebApplication1.Infrastructure.Context;

public class ProductConfiguration
{
    public ProductConfiguration(EntityTypeBuilder<Product> entityBuilder)
    {
        entityBuilder
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
    }
}