namespace WebApplication1.Infrastructure.Repositories;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    int Complete();
}