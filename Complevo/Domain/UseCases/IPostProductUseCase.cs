using Complevo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Complevo.Domain.UseCases;

public interface IPostProductUseCase
{
  Task<bool> Handle(Product product);
}