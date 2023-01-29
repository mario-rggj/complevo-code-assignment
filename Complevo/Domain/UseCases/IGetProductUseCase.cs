using Microsoft.AspNetCore.Mvc;
using Complevo.Domain.Models;

namespace Complevo.Domain.UseCases;

public interface IGetProductUseCase
{
  Task<ActionResult<Product>> Handle(int id);
}