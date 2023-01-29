namespace Complevo.Domain.UseCases;

public interface IDeleteProductUseCase
{
  Task Handle(int id);
}