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

  public async Task Handle(int id)
  {
    var product = await _unitOfWork.Products.Get(id);
    if (product.Value == null) throw new Exception("Product not found");

    _unitOfWork.Products.Remove(product.Value);
    await _unitOfWork.Complete();
  }
}