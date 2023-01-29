using Microsoft.AspNetCore.Mvc;
using Complevo.Domain.Models;

namespace Complevo.Domain.UseCases;

public interface IGetProductsUseCase
{
  Task<ActionResult<IEnumerable<Product>>> Handle();
}