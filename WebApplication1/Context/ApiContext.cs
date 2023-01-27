using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain;

namespace WebApplication1.Context;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
        
        base.OnModelCreating(modelBuilder);
    }
}