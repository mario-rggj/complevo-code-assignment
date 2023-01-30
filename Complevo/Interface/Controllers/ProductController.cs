using Microsoft.AspNetCore.Mvc;
using Complevo.Domain.Models;
using Complevo.Domain.UseCases;
using Complevo.Interface.Dtos.ProductDtos;

namespace Complevo.Interface.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
  private readonly IDeleteProductUseCase _deleteProductUseCase;
  private readonly IGetProductsUseCase _getProductsUseCase;
  private readonly IGetProductUseCase _getProductUseCase;
  private readonly IPostProductUseCase _postProductUseCase;
  private readonly IPutProductUseCase _putProductUseCase;

  public ProductController(
      IGetProductsUseCase getProductsUseCase,
      IGetProductUseCase getProductUseCase,
      IPutProductUseCase putProductUseCase,
      IPostProductUseCase postProductUseCase,
      IDeleteProductUseCase deleteProductUseCase)
  {
    _getProductsUseCase = getProductsUseCase;
    _getProductUseCase = getProductUseCase;
    _putProductUseCase = putProductUseCase;
    _postProductUseCase = postProductUseCase;
    _deleteProductUseCase = deleteProductUseCase;
  }

  // GET: api/Product

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int page = 1, int pageSize = 5)
  {
    return await _getProductsUseCase.Handle(page, pageSize);
  }

  // GET: api/Product/5
  [HttpGet("{id}")]
  public async Task<ActionResult<Product>> GetProduct(int id)
  {
    var product = await _getProductUseCase.Handle(id);

    return product.Value is null ? NotFound() : product;
  }

  // PUT: api/Product/5
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  [HttpPut("{id}")]
  public async Task<IActionResult> PutProduct(int id, Product product)
  {
    if (id != product.Id) return BadRequest();

    if (await _putProductUseCase.Handle(id, product))
      return NoContent();
    return NotFound();
  }

  // POST: api/Product
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  [HttpPost]
  public async Task<ActionResult<Product>> PostProduct([FromBody] PostProductDto productDto)
  {
    var product = productDto.CastToDomainClass();

    if (await _postProductUseCase.Handle(product))
      return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    return Conflict();
  }

  // DELETE: api/Product/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteProduct(int id)
  {
    if (await _deleteProductUseCase.Handle(id))
      return NoContent();
    return NotFound();
  }
}