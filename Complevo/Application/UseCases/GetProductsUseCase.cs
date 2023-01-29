using Microsoft.AspNetCore.Mvc;
using Complevo.Domain.Infrastructure;
using Complevo.Domain.Models;
using Complevo.Domain.UseCases;

namespace Complevo.Application.UseCases;

public class GetProductsUseCase : IGetProductsUseCase
{
  private readonly IUnitOfWork _unitOfWork;

  public GetProductsUseCase(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<ActionResult<IEnumerable<Product>>> Handle()
  {
    return await _unitOfWork.Products.GetAll();
  }
}