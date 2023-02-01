using Complevo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Complevo.Domain.UseCases;

public interface IGetProductUseCase
{
  Task<Product> Handle(int id);
}