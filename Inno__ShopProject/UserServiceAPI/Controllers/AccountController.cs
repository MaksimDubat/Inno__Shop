using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserServiceAPI.Interface;
using UserServiceAPI.JwtSet.JwtAttribute;
using UserServiceAPI.LoginModels;
using UserServiceAPI.RegistartionModels;

namespace UserServiceAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [JwtAuthorize(Roles = "Admin")]
        [HttpGet("adminonly")]
        public IActionResult AdminOnly()
        {
            return Ok("Admin mode");
        }

        [JwtAuthorize(Roles = "User")]
        [HttpGet("useronly")]
        public IActionResult UserOnly()
        {
            return Ok("User mode");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model, CancellationToken cancellation)
        {
            try
            {
                var token = await _authenticationService.SignInAsync(model.Email, model.Password, cancellation);
                return Ok(new { Token = token });
            } 
            catch (UnauthorizedAccessException)
            {
               return Unauthorized("Invalid");
            }
           
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellation)
        {
            await _authenticationService.SignOutAsync(cancellation);
            return Ok();
        }

       
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationModel model, CancellationToken cancellation)
        {
            var result = await _authenticationService.RegisterAsync(model.Email,model.Name, model.Password, cancellation);
            if (result.Succeeded)
            {
                var token = await _authenticationService.SignInAsync(model.Email, model.Password, cancellation);
                return Ok(new {Message =" reg god", Token = token});
            }
            else
            {
                var errors = result.Errors?.Select(x => x.Description).ToList();
                return BadRequest(errors);
            }
        }
    }
}
