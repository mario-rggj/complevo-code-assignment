using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Models;
using WebApplication1.Infrastructure.Context;

namespace WebApplication1.Application.UseCases;

public class GetProductsUseCase
{
    private readonly ApiContext _context;

    public GetProductsUseCase(ApiContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<Product>>> Handle()
    {
        return await _context.Products.ToListAsync();
    }
}