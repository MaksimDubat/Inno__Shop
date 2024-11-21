using Microsoft.AspNetCore.Mvc;
using ProductServiceAPI.Entities;
using ProductServiceAPI.Interface;

namespace ProductServiceAPI.Controllers
{

    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts(CancellationToken cancellation)
        {
            var product = await _productRepository.GetAllAsync(cancellation);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id, CancellationToken cancellation)
        {
            var product = await _productRepository.GetAsync(id, cancellation);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost("Add")]
        public async Task<ActionResult<Product>> AddProduct(Product product, CancellationToken cancellation)
        {
            await _productRepository.AddAsync(product, cancellation);
            return CreatedAtAction(nameof(GetById), new { id = product.Id });
        }
        [HttpPost("update/{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(Product product, int id, CancellationToken cancellation)
        {
            if (id != product.Id)
            {
                return BadRequest("error");
            }
            await _productRepository.UpdateAsync(product, cancellation);
            return Ok("updated");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id, CancellationToken cancellation)
        {
            var product = await _productRepository.GetAsync(id, cancellation);
            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteAsync(id, cancellation);
            return Ok("deleted");
        }
        [HttpGet("byname")]
        public async Task<ActionResult<Product>> GetProductName(string name, int id, CancellationToken cancellation)
        {
            var product = await _productRepository.GetAsync(id, cancellation);
            if (product == null)
            {
                return NotFound();
            }
            var productName = await _productRepository.GetByNameAsync(name, cancellation);
            if (productName == null)
            {
                return NotFound();
            }
            return Ok(productName);
        }
    }
}
