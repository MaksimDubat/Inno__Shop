using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using UserServiceAPI.Application.JwtSet.JwtAttribute;
using UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Commands;
using UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Queries;
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
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <param name="cancellation"></param>
        [HttpGet("All")]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<AppUsers>>> GetAllUsers(CancellationToken cancellation)
        {
           var query = new GetAllUsersQuery();
           var result = await _mediator.Send(query, cancellation);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
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
            var query = new GetUserByIdQuery(id);
            var result =await  _mediator.Send(query,cancellation);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
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
            var command = new AddUserCommand(user);
            var result = await _mediator.Send(command, cancellation);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
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

            var command = new UpdateUserCommand(id,user);
            var result = await _mediator.Send(command,cancellation);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
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
           var command = new DeleteUserCommand(id);
           var result = await _mediator.Send(command.Id,cancellation);
           return Ok(result);
        }
        /// <summary>
        /// Получение Email пользователя.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellation"></param>
        /// <param name="id"></param>
        [HttpGet("byemail")]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<ActionResult<AppUsers>> GetUserEmail(string email, CancellationToken cancellation)
        {
            var query = new GetUserByEmailQuery(email);
            var result = await _mediator.Send(query,cancellation);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


    }
}
