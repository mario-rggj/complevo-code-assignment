using WebApplication1.Domain.Models;
using WebApplication1.Infrastructure.Context;

namespace WebApplication1.Application.UseCases;

public class PostProductUseCase
{
    private readonly ApiContext _context;

    public PostProductUseCase(ApiContext context)
    {
        _context = context;
    }

    public async Task Handle(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }
}