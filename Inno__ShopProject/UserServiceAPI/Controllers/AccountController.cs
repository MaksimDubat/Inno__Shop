using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserServiceAPI.Interface;
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
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model, CancellationToken cancellation)
        {
            if (await _authenticationService.SignInAsync(model.Email, model.Password, cancellation))
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
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
                await _authenticationService.SignInAsync(model.Email, model.Password, cancellation);
                return Ok("Reg");
            }
            else
            {
                var errors = result.Errors?.Select(x => x.Description).ToList();
                return BadRequest(errors);
            }
        }
    }
}
