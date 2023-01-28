using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Infrastructure;
using WebApplication1.Domain.Models;
using WebApplication1.Domain.UseCases;

namespace WebApplication1.Application.UseCases;

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
            var instanceDoesntExist = (await _unitOfWork.Products.Get(id)) is null;
            if (instanceDoesntExist)
            {
                throw new Exception("Product not found");
            }
            else
            {
                throw;
            }
        }
    }
}