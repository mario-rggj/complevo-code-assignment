using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain;

namespace WebApplication1.Infrastructure.Context;

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