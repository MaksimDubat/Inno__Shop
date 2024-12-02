using Microsoft.AspNetCore.Mvc;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.Entities;
using ProductServiceAPI.HttpExtensions;
using ProductServiceAPI.Interface;
using ProductServiceAPI.Model.Filters;
using UserServiceAPI.Interface;
using UserServiceAPI.JwtSet.JwtAttribute;

namespace ProductServiceAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с продуктом.
    /// </summary>
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductFiltration _productFiltration;
        private readonly MutableInnoShopProductDbContext _mutableDbContext;
        private readonly IUserRepository _userRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductController(IProductRepository productRepository, IProductFiltration productFiltration, MutableInnoShopProductDbContext mutableDbContext, 
            IUserRepository userRepository, IHttpClientFactory httpClientFactory)
        {
            _productRepository = productRepository;
            _productFiltration = productFiltration;
            _mutableDbContext = mutableDbContext;
            _userRepository = userRepository;
            _httpClientFactory = httpClientFactory;
       
        }
        /// <summary>
        /// Получение всех продуктов.
        /// </summary>
        /// <param name="cancellation"></param>
        [HttpGet("all")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts(CancellationToken cancellation)
        {
            var product = await _productRepository.GetAllAsync(cancellation);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        /// <summary>
        /// Получение продукта по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        [HttpGet("{id}")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<ActionResult<Product>> GetById(int id, CancellationToken cancellation)
        {
            var product = await _productRepository.GetAsync(id, cancellation);
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
        public async Task<ActionResult<Product>> AddProduct(Product product, CancellationToken cancellation)
        {
            var userId = HttpContext.GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }
            await _productRepository.AddAsync(product, cancellation);
            return Ok($"added {product.Name}");
        }
        /// <summary>
        /// Обновление продукта.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        [HttpPost("update/{id}")]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> UpdateProduct(Product product, int id, CancellationToken cancellation)
        {
            var userId = HttpContext.GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }
            if (id != product.Id)
            {
                return BadRequest("error");
            }
            await _productRepository.UpdateAsync(product, cancellation);
            return Ok("updated");
        }
        /// <summary>
        /// Удаление продукта.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        [HttpDelete("{id}")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<ActionResult<Product>> DeleteProduct(int id, CancellationToken cancellation)
        {
            var userId = HttpContext.GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }
            var product = await _productRepository.GetAsync(id, cancellation);
            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteAsync(id, cancellation);
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
        /// <summary>
        /// Использование фильтрации продукта.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellation"></param>
        [HttpGet("filter")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetFilteredProduct ([FromQuery] ProductFiltersModel filter, CancellationToken cancellation)
        {
            var product = await _productFiltration.GetFilteredProductsAsync(filter, cancellation);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        /// <summary>
        /// Статус продукта относительно пользователя.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isActive"></param>
        /// <param name="cancellation"></param>
        [HttpPost("status")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<IActionResult> SetProductStatus(int userId, bool isActive, CancellationToken cancellation)
        {
            var products = await _productRepository.GetAsync(userId, cancellation);
            if(products == null)
            {
                return NotFound("no user products");
            }
            foreach (var product in products)
            {
                product.IsActive = isActive;
            }
            await _mutableDbContext.SaveChangesAsync(); 
            return Ok("ststus updated");
        }
        /// <summary>
        /// Деактивация пользователя.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        [HttpPost("deactivateUser/{id}")]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> DeactivateUser(int id, CancellationToken cancellation)
        {
            var user = await _userRepository.GetAsync(id, cancellation);
            if (user == null)
            {
                return NotFound();
            }
            user.IsActive = false;
            await _mutableDbContext.SaveChangesAsync(cancellation);
            
            var httpClient = _httpClientFactory.CreateClient("ProductService");
            var response = await httpClient.PostAsJsonAsync("api/product/status",
                new { id, isActive = false }, cancellation);
            if(!response.IsSuccessStatusCode)
            {
                return BadRequest("deactivation error");
            }
            return Ok("diactivation ok");
        }

    }
}
