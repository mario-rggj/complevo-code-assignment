using WebApplication1.Domain.Infrastructure;
using WebApplication1.Domain.Models;
using WebApplication1.Domain.UseCases;

namespace WebApplication1.Application.UseCases;

public class PostProductUseCase : IPostProductUseCase
{
  private readonly IUnitOfWork _unitOfWork;

  public PostProductUseCase(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(Product product)
  {
    _unitOfWork.Products.Add(product);
    await _unitOfWork.Complete();
  }
}