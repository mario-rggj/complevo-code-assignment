using WebApplication1.Infrastructure.Context;

namespace WebApplication1.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApiContext _context;
    public IProductRepository Products { get; private set; }

    public UnitOfWork(ApiContext context)
    {
        _context = context;
        Products = new ProductRepository(_context);
    }
    
    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}