using Complevo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Complevo.Domain.UseCases;

public interface IGetProductUseCase
{
  Task<ActionResult<Product>> Handle(int id);
}