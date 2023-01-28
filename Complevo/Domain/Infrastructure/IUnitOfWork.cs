using WebApplication1.Domain.Infrastructure.Repositories;

namespace WebApplication1.Domain.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    Task<int> Complete();
}