using Complevo.Domain.Infrastructure;
using Complevo.Domain.UseCases;

namespace Complevo.Application.UseCases;

public class DeleteProductUseCase : IDeleteProductUseCase
{
  private readonly IUnitOfWork _unitOfWork;

  public DeleteProductUseCase(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<bool> Handle(int id)
  {
    var product = await _unitOfWork.Products.Get(id);
    if (product.Value == null) return false;

    _unitOfWork.Products.Remove(product.Value);
    await _unitOfWork.Complete();
    return true;
  }
}