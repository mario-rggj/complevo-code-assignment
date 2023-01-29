using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain.Infrastructure;
using WebApplication1.Domain.Models;
using WebApplication1.Domain.UseCases;

namespace WebApplication1.Application.UseCases;

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