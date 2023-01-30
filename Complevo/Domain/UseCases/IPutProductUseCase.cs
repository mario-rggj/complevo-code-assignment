using Complevo.Domain.Models;

namespace Complevo.Domain.UseCases;

public interface IPutProductUseCase
{
  Task<bool> Handle(int id, Product product);
}