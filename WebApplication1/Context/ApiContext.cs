using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain;

namespace WebApplication1.Context;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}