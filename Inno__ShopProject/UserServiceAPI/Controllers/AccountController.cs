using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserServiceAPI.Interface;
using UserServiceAPI.LoginView.Model;
using UserServiceAPI.RegistartionView.Model;

namespace UserServiceAPI.Controllers
{
    //[ApiController]
    //[Route("api/auth")]
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return PartialView("_loginPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellation)
        {
            if (await _authenticationService.SignInAsync(model.Email, model.Password, cancellation))
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        public async Task<IActionResult> Logout(CancellationToken cancellation)
        {
            await _authenticationService.SignOutAsync(cancellation);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return PartialView("_RegisterPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string name, CancellationToken cancellation)
        {
            var result = await _authenticationService.RegisterAsync(model.Email,model.Name, model.Password, cancellation);
            if (result.Succeeded)
            {
                await _authenticationService.SignInAsync(model.Email, model.Password, cancellation);
                return RedirectToAction("Index", "Home");
            }

            var errors = result.Errors?.Select(x => x.Description).ToList() ?? [];

            return PartialView("_RegisterPartial", new RegisterViewModel
            {
                Errors = errors
            });
        }
    }
}
