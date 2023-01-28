using System.Collections;
using NuGet.Protocol.Core.Types;
using WebApplication1.Domain;
using WebApplication1.Infrastructure.Context;

namespace WebApplication1.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApiContext context) : base(context)
    {
    }

    public IEnumerable<Product> GetAllProductsUnderPrice10()
    {
        return Context.Products.Where(p => p.Price < 10m).ToList();
    }
}