using Complevo.Domain.Models;

namespace Complevo.Domain.UseCases;

public interface IPostProductUseCase
{
  Task<bool> Handle(Product product);
}