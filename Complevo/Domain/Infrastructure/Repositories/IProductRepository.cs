using Complevo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Complevo.Domain.Infrastructure.Repositories;

public interface IProductRepository : IRepository<Product>
{
  Task<IEnumerable<Product>> GetAllProductsUnderPrice10();
}