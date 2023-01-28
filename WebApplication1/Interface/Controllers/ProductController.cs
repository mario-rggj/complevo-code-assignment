using Microsoft.AspNetCore.Mvc;
using WebApplication1.Application.UseCases;
using WebApplication1.Domain.Models;

// TODO: create Patch Method
// TODO: create class for Product not found exception

namespace WebApplication1.Interface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly GetProductsUseCase _getProductsUseCase;
        private readonly GetProductUseCase _getProductUseCase;
        private readonly PutProductUseCase _putProductUseCase;
        private readonly PostProductUseCase _postProductUseCase;
        private readonly DeleteProductUseCase _deleteProductUseCase;

        public ProductController(
            GetProductsUseCase getProductsUseCase,
            GetProductUseCase getProductUseCase,
            PutProductUseCase putProductUseCase,
            PostProductUseCase postProductUseCase,
            DeleteProductUseCase deleteProductUseCase)
        {
            _getProductsUseCase = getProductsUseCase;
            _getProductUseCase = getProductUseCase;
            _putProductUseCase = putProductUseCase;
            _postProductUseCase = postProductUseCase;
            _deleteProductUseCase = deleteProductUseCase;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _getProductsUseCase.Handle();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _getProductUseCase.Handle(id);

            return product == null ? NotFound() : product;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                await _putProductUseCase.Handle(id, product);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Product not found"))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _postProductUseCase.Handle(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _deleteProductUseCase.Handle(id);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Product not found"))
                    return NotFound();
                throw;
            }

            return NoContent();
        }
    }
}