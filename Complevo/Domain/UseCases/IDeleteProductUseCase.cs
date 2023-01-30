namespace Complevo.Domain.UseCases;

public interface IDeleteProductUseCase
{
  Task<bool> Handle(int id);
}