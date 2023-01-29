using Complevo.Domain.Models;

namespace Complevo.Domain.UseCases;

public interface IPostProductUseCase
{
  Task Handle(Product product);
}