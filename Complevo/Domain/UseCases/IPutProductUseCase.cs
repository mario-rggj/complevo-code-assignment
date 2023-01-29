using WebApplication1.Domain.Models;

namespace WebApplication1.Domain.UseCases;

public interface IPutProductUseCase
{
  Task Handle(int id, Product product);
}