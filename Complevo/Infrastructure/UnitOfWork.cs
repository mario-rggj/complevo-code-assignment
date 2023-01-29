using Complevo.Domain.Infrastructure;
using Complevo.Domain.Infrastructure.Repositories;
using Complevo.Infrastructure.Context;
using Complevo.Infrastructure.Repositories;

namespace Complevo.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
  private readonly ApiContext _context;

  public UnitOfWork(ApiContext context)
  {
    _context = context;
    Products = new ProductRepository(_context);
  }

  public IProductRepository Products { get; }

  public Task<int> Complete()
  {
    return _context.SaveChangesAsync();
  }

  public void Dispose()
  {
    _context.DisposeAsync();
  }
}