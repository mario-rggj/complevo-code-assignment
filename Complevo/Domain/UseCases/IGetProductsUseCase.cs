using Complevo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Complevo.Domain.UseCases;

public interface IGetProductsUseCase
{
  Task<ActionResult<IEnumerable<Product>>> Handle(int pageIndex, int pageSize);
}