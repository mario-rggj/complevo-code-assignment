using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Infrastructure.Repositories;
using WebApplication1.Domain.Models;
using WebApplication1.Infrastructure.Context;

namespace WebApplication1.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
  public ProductRepository(ApiContext context) : base(context)
  {
  }

  public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsUnderPrice10()
  {
    return await Context.Products.Where(p => p.Price < 10m).ToListAsync();
  }
}