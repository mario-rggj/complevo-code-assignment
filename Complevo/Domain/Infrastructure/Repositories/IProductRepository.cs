using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain.Models;

namespace WebApplication1.Domain.Infrastructure.Repositories;

public interface IProductRepository : IRepository<Product>
{
  Task<ActionResult<IEnumerable<Product>>> GetAllProductsUnderPrice10();
}