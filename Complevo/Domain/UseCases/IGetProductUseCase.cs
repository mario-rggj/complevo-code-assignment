using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain.Models;

namespace WebApplication1.Domain.UseCases;

public interface IGetProductUseCase
{
    Task<ActionResult<Product>> Handle(int id);
}