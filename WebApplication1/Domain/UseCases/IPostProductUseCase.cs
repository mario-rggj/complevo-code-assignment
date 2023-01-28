using WebApplication1.Domain.Models;

namespace WebApplication1.Domain.UseCases;

public interface IPostProductUseCase
{
    Task Handle(Product product);
}