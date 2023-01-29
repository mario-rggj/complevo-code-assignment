using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain.Infrastructure;
using WebApplication1.Domain.Models;
using WebApplication1.Domain.UseCases;

namespace WebApplication1.Application.UseCases;

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