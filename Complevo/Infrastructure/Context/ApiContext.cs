using Microsoft.EntityFrameworkCore;
using Complevo.Domain.Models;

namespace Complevo.Infrastructure.Context;

public class ApiContext : DbContext
{
  public ApiContext(DbContextOptions<ApiContext> options) : base(options)
  {
  }

  public DbSet<Product> Products { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    new ProductConfiguration(modelBuilder.Entity<Product>());
  }
}