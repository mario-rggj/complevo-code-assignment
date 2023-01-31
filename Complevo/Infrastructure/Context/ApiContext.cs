using Complevo.Domain.Models;
using Microsoft.EntityFrameworkCore;

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

  public override int SaveChanges()
  {
    AddTimestamps();
    return base.SaveChanges();
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
  {
    AddTimestamps();
    return await base.SaveChangesAsync(cancellationToken);
  }

  private void AddTimestamps()
  {
    var entities = ChangeTracker.Entries()
      .Where(e => e.Entity is BaseModel
                  && e.State is EntityState.Added or EntityState.Modified);

    foreach (var entity in entities)
    {
      var now = DateTime.UtcNow;

      entity.Property("CreatedAt").IsModified = false;
      if (entity.State is EntityState.Added)
      {
        ((BaseModel)entity.Entity).CreatedAt = now;
        entity.Property("CreatedAt").IsModified = true;
      }

      ((BaseModel)entity.Entity).UpdatedAt = now;
    }
  }
}