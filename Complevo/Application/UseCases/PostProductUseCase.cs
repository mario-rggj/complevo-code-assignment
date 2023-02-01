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

  public async Task<bool> Handle(Product product)
  {
    var persistedProduct = (await _unitOfWork.Products
      .Find(p => p.Name == product.Name)).ToList().Count;
    if (persistedProduct >= 1) return false;
    _unitOfWork.Products.Add(product);
    await _unitOfWork.Complete();
    return true;
  }
}