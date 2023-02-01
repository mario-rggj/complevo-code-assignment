using Complevo.Domain.Models;
using Complevo.Domain.UseCases;
using Complevo.Interface.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;

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
  public async Task<ActionResult<IEnumerable<GetProductDto>>> GetProducts(int page = 1, int pageSize = 5)
  {
    var products = await _getProductsUseCase.Handle(page, pageSize);
    return GetProductDto.fromProducts(products);
  }

  // GET: api/Product/5
  [HttpGet("{id}")]
  public async Task<ActionResult<GetProductDto>> GetProduct(int id)
  {
    var product = await _getProductUseCase.Handle(id);

    return product == null ? NotFound() : new GetProductDto(product);
  }

  // PUT: api/Product/5
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  [HttpPut("{id}")]
  public async Task<IActionResult> PutProduct(int id, PutProductDto product)
  {
    if (id != product.Id) return BadRequest();

    if (await _putProductUseCase.Handle(id, product.toProduct()))
      return NoContent();
    return NotFound();
  }

  // POST: api/Product
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  [HttpPost]
  public async Task<ActionResult<GetProductDto>> PostProduct([FromBody] PostProductDto productDto)
  {
    var product = productDto.ToProduct();

    if (await _postProductUseCase.Handle(product))
      return CreatedAtAction("GetProduct", new { id = product.Id }, new GetProductDto(product));
    return Conflict($"Product with name: \"{product.Name}\" already exists");
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