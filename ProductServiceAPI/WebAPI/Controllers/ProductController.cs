using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductServiceAPI.Application.MediatrConfig.Commands;
using ProductServiceAPI.Application.MediatrConfig.Queries;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.HttpExtensions;
using ProductServiceAPI.Model.Filters;
using UserServiceAPI.Application.JwtSet.JwtAttribute;

namespace ProductServiceAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с продуктом.
    /// </summary>
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получение всех продуктов.
        /// </summary>
        /// <param name="cancellation"></param>
        [HttpGet("all")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts(CancellationToken cancellation)
        {
            var products = await _mediator.Send(new GetAllProductsQuery(), cancellation);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        /// <summary>
        /// Получение продукта по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        [HttpGet("get/{id}")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<ActionResult<Product>> GetById(int id, CancellationToken cancellation)
        {
            var query = new GetByIdQuery { Id = id };
            var product = await _mediator.Send(query, cancellation);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        /// <summary>
        /// Добавление продукта.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="cancellation"></param>
        [HttpPost("Add")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<ActionResult<Product>> AddProduct([FromBody] AddProductCommand command, CancellationToken cancellation)
        {
            var userId = HttpContext.GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }
            var result = await _mediator.Send(command, cancellation);
            return Ok(result);
        }
        /// <summary>
        /// Обновление продукта.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        [HttpPost("update/{id}")]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] UpdateProductCommand command, CancellationToken cancellation)
        {
            var userId = HttpContext.GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }
            var updatedProduct = await _mediator.Send(command, cancellation);
            if (id != command.Id)
            {
                return NotFound("error");
            }
            return Ok(updatedProduct);
        }
        /// <summary>
        /// Удаление продукта.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        [HttpDelete("delete/{id}")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<ActionResult<Product>> DeleteProduct(int id, CancellationToken cancellation)
        {
            var userId = HttpContext.GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command, cancellation);
            if (!result)
            {
                return NotFound();
            }
            return Ok("deleted");
        }
        /// <summary>
        /// Получение наименования продукта.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        [HttpGet("byname")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<ActionResult<Product>> GetProductName(string name, CancellationToken cancellation)
        {
            var query = new GetProductNameQuery(name);
            var product = await _mediator.Send(query, cancellation);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        /// <summary>
        /// Использование фильтрации продукта.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellation"></param>
        [HttpGet("filter")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetFilteredProduct([FromQuery] ProductFiltersModel filter, CancellationToken cancellation)
        {
            var query = new GetFilteredProductQuery(filter);
            var filteredProducts = await _mediator.Send(query, cancellation);
            if (filteredProducts == null)
            {
                return NotFound();
            }
            return Ok(filteredProducts);
        }
        /// <summary>
        /// Статус продукта относительно пользователя.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isActive"></param>
        /// <param name="cancellation"></param>
        [HttpPost("status/{id}")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<IActionResult> SetProductStatus(int userId, [FromBody] bool isActive, CancellationToken cancellation)
        {
            var command = new SetProductStatusCommand(userId, isActive);
            var result = await _mediator.Send(command, cancellation);
            if (!result)
            {
                return NotFound();
            }
            return Ok("done");
        }
    }
}
