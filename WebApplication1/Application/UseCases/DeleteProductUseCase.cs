using WebApplication1.Domain.UseCases;
using WebApplication1.Infrastructure.Context;

namespace WebApplication1.Application.UseCases;

public class DeleteProductUseCase : IDeleteProductUseCase
{
    private readonly ApiContext _context;

    public DeleteProductUseCase(ApiContext context)
    {
        _context = context;
    }

    public async Task Handle(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}