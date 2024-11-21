using Microsoft.AspNetCore.Mvc;
using UserServiceAPI.Entities;
using UserServiceAPI.Interface;

namespace UserServiceAPI.Controllers
{

    
    [ApiController]
    [Route("api/users")]
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
                return NotFound();
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
        [HttpPost("add")]
        public async Task<ActionResult<AppUsers>> AddUser(AppUsers user, CancellationToken cancellation)
        {
            await _userRepository.AddAsync(user, cancellation);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<AppUsers>> UpdateUser(AppUsers user, CancellationToken cancellation, int id)  
        {
            if (id != user.UserId)
            {
                return BadRequest("error");
            }
            await _userRepository.UpdateAsync(user, cancellation);
            return Ok("updated");
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
           return Ok("deleted");
        }
        [HttpGet("byemail")]
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
                return NotFound();
            }
            return Ok(userEmail);
        }
        
    }
}   
