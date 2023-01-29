using Complevo.Domain.Infrastructure;
using Complevo.Domain.Models;
using Complevo.Domain.UseCases;

namespace Complevo.Application.UseCases;

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