using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Models;
using WebApplication1.Infrastructure.Context;

namespace WebApplication1.Application.UseCases;

public class PutProductUseCase
{
    private readonly ApiContext _context;

    public PutProductUseCase(ApiContext context)
    {
        _context = context;
    }

    public async Task Handle(int id, Product product)
    {
        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(_context.Products?.Any(e => e.Id == id)).GetValueOrDefault())
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