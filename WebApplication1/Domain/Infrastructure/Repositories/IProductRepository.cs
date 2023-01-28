using WebApplication1.Domain.Models;

namespace WebApplication1.Domain.Infrastructure.Repositories;

public interface IProductRepository : IRepository<Product>
{
        IEnumerable<Product> GetAllProductsUnderPrice10();
}