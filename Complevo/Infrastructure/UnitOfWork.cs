using WebApplication1.Domain.Infrastructure;
using WebApplication1.Domain.Infrastructure.Repositories;
using WebApplication1.Infrastructure.Context;
using WebApplication1.Infrastructure.Repositories;

namespace WebApplication1.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApiContext _context;
    public IProductRepository Products { get; private set; }

    public UnitOfWork(ApiContext context)
    {
        _context = context;
        Products = new ProductRepository(_context);
    }
    
    public Task<int> Complete()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.DisposeAsync();
    }
}