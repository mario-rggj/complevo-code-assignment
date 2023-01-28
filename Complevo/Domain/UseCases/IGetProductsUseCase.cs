using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain.Models;

namespace WebApplication1.Domain.UseCases;

public interface IGetProductsUseCase
{
    Task<ActionResult<IEnumerable<Product>>> Handle();
}