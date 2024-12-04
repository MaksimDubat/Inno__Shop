using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using UserServiceAPI.Application.JwtSet.JwtAttribute;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;

namespace UserServiceAPI.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователем.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <param name="cancellation"></param>
        [HttpGet("All")]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<AppUsers>>> GetAllUsers(CancellationToken cancellation)
        {
            var users = await _userRepository.GetAllAsync(cancellation);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        /// <summary>
        /// Получение пользователя по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        [HttpGet("{id}")]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<ActionResult<AppUsers>> GetUserById(int id, CancellationToken cancellation)
        {
            var user = await _userRepository.GetAsync(id, cancellation);
            if (user == null)
            {
                return NotFound("not admin");
            }
            return Ok(user);
        }

        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellation"></param>
        [HttpPost("add")]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<ActionResult<AppUsers>> AddUser(AppUsers user, CancellationToken cancellation)
        {
            await _userRepository.AddAsync(user, cancellation);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        /// <summary>
        /// Обновление пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellation"></param>
        /// <param name="id"></param>
        [HttpPost("update/{id}")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<ActionResult<AppUsers>> UpdateUser(AppUsers user, CancellationToken cancellation, int id)
        {
            if (id != user.UserId)
            {
                return BadRequest("error");
            }

            await _userRepository.UpdateAsync(user, cancellation);
            return Ok("updated");
        }
        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        [HttpDelete("{id}")]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<ActionResult<AppUsers>> DeletUser(int id, CancellationToken cancellation)
        {
            var user = await _userRepository.GetAsync(id, cancellation);
            if (user == null)
            {
                return NotFound("not admin");
            }
            await _userRepository.DeleteAsync(id, cancellation);
            return Ok("deleted");
        }
        /// <summary>
        /// Получение Email пользователя.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellation"></param>
        /// <param name="id"></param>
        [HttpGet("byemail")]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<ActionResult<AppUsers>> GetUserEmail(string email, CancellationToken cancellation, int id)
        {
            var user = await _userRepository.GetAsync(id, cancellation);
            if (user == null)
            {
                return NotFound("not admin");
            }
            var userEmail = await _userRepository.GetUserByEmailAsync(email, cancellation);
            if (userEmail == null)
            {
                return NotFound();
            }
            return Ok(userEmail);
        }


    }
}
