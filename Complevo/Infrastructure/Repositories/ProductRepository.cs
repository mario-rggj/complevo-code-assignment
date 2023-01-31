using Complevo.Domain.Infrastructure.Repositories;
using Complevo.Domain.Models;
using Complevo.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Complevo.Infrastructure.Repositories;

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