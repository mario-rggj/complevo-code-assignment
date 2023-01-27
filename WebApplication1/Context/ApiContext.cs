using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Context;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
    }
}