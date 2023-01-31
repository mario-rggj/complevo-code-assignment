using Complevo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Complevo.Domain.Infrastructure.Repositories;

public interface IProductRepository : IRepository<Product>
{
  Task<ActionResult<IEnumerable<Product>>> GetAllProductsUnderPrice10();
}