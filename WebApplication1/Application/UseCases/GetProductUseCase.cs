using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain;
using WebApplication1.Infrastructure.Context;

namespace WebApplication1.Application.UseCases;

public class GetProductUseCase
{
    private readonly ApiContext _context;
    
    public GetProductUseCase(ApiContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<Product>> Handle(int id)
    {
        return await _context.Products.FindAsync(id);
    }
}