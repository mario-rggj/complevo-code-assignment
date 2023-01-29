using Microsoft.EntityFrameworkCore;
using Complevo.Domain.Infrastructure;
using Complevo.Domain.Models;
using Complevo.Domain.UseCases;

namespace Complevo.Application.UseCases;

public class PutProductUseCase : IPutProductUseCase
{
  private readonly IUnitOfWork _unitOfWork;

  public PutProductUseCase(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(int id, Product product)
  {
    _unitOfWork.Products.Entry(product).State = EntityState.Modified;

    try
    {
      await _unitOfWork.Complete();
    }
    catch (DbUpdateConcurrencyException)
    {
      var instanceDoesntExist = (await _unitOfWork.Products.Get(id)).Result is null;
      if (instanceDoesntExist)
        throw new Exception("Product not found");
      throw;
    }
  }
}