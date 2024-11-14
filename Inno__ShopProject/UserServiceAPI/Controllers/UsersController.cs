using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Npgsql.TypeMapping;
using UserServiceAPI.Entities;
using UserServiceAPI.Interface;

namespace UserServiceAPI.Controllers
{

    
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<AppUsers>>> GetAllUsers(CancellationToken cancellation)
        {
            var users = await _userRepository.GetAllAsync(cancellation);
            if (users == null)
            {
                return NoContent();
            }
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUsers>> GetUserById(int id, CancellationToken cancellation)
        {
            var user = await _userRepository.GetAsync(id, cancellation);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<AppUsers>> AddUser(AppUsers user, CancellationToken cancellation)
        {
            await _userRepository.AddAsync(user, cancellation);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(AppUsers user, CancellationToken cancellation, int id)
        {
            if (id != user.UserId)
            {
                return BadRequest("error");
            }
            await _userRepository.UpdateAsync(user, cancellation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AppUsers>> DeletUser (int id, CancellationToken cancellation)
        {
           var user = await  _userRepository.GetAsync(id,cancellation);
           if (user == null)
           {
               return NotFound();
           }
           await _userRepository.DeleteAsync(id, cancellation);
           return NoContent();
        }
        [HttpGet("by-email")]
        public async Task<ActionResult<AppUsers>> GetUserEmail(string email, CancellationToken cancellation, int id)
        {
            var user = await _userRepository.GetAsync(id, cancellation);
            if (user == null)
            {
                return NotFound();
            }
            var userEmail = await _userRepository.GetUserByEmailAsync(email, cancellation);
            if (userEmail == null)
            {
                return NoContent();
            }
            return Ok(userEmail);
        }
        
    }
}   
