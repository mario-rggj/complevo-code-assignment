using WebApplication1.Domain;

namespace WebApplication1.Infrastructure.Repositories;

public interface IProductRepository : IRepository<Product>
{
        IEnumerable<Product> GetAllProductsUnderPrice10();
}