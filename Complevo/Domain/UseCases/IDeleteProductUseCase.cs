namespace WebApplication1.Domain.UseCases;

public interface IDeleteProductUseCase
{
  Task Handle(int id);
}