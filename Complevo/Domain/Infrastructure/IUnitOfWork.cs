using Complevo.Domain.Infrastructure.Repositories;

namespace Complevo.Domain.Infrastructure;

public interface IUnitOfWork : IDisposable
{
  IProductRepository Products { get; }
  Task<int> Complete();
}