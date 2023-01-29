using Complevo.Domain.Models;

namespace Complevo.Domain.UseCases;

public interface IPutProductUseCase
{
  Task Handle(int id, Product product);
}