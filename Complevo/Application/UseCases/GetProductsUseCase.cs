using Complevo.Domain.Infrastructure;
using Complevo.Domain.Models;
using Complevo.Domain.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Complevo.Application.UseCases;

public class GetProductsUseCase : IGetProductsUseCase
{
  private readonly IUnitOfWork _unitOfWork;

  public GetProductsUseCase(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<IEnumerable<Product>> Handle(int pageIndex, int pageSize)
  {
    return await _unitOfWork.Products.GetAll(pageIndex, pageSize);
  }
}