using Complevo.Domain.Infrastructure;
using Complevo.Domain.Models;
using Complevo.Domain.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Complevo.Application.UseCases;

public class GetProductUseCase : IGetProductUseCase
{
  private readonly IUnitOfWork _unitOfWork;

  public GetProductUseCase(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<ActionResult<Product>> Handle(int id)
  {
    return await _unitOfWork.Products.Get(id);
  }
}