using Microsoft.AspNetCore.Mvc;
using Complevo.Domain.Models;

namespace Complevo.Domain.Infrastructure.Repositories;

public interface IProductRepository : IRepository<Product>
{
  Task<ActionResult<IEnumerable<Product>>> GetAllProductsUnderPrice10();
}