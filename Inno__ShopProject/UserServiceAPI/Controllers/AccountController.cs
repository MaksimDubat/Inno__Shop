using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserServiceAPI.DataBaseAccess;
using UserServiceAPI.Entities;
using UserServiceAPI.Interface;
using UserServiceAPI.JwtSet.JwtAttribute;
using UserServiceAPI.Models.LoginModels;
using UserServiceAPI.Models.PasswordResetModels;
using UserServiceAPI.Models.RegistartionModels;

namespace UserServiceAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<AppUsers> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly MutableInnoShopDbContext _dbContext;

        public AccountController(IAuthenticationService authenticationService, UserManager<AppUsers> userManager, IEmailSender emailSender, MutableInnoShopDbContext dbContext)
        {

            _authenticationService = authenticationService;
            _userManager = userManager;
            _emailSender = emailSender;
            _dbContext = dbContext;
        }

        [HttpGet("adminonly")]
        [JwtAuthorize(Roles = "Admin")]
        public IActionResult AdminOnly()
        {
            return Ok("Admin mode");
        }

        
        [HttpGet("useronly")]
        [JwtAuthorize(Roles = "User")]
        public IActionResult UserOnly()
        {
            return Ok("User mode");
        }

        
        [HttpPost("login")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<IActionResult> Login(LoginModel model, CancellationToken cancellation)
        {
            try
            {
                var token = await _authenticationService.SignInAsync(model.Email, model.Password, cancellation); 
                return Ok("good");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid");
            }

        }

        
        [HttpPost("logout")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<IActionResult> Logout(CancellationToken cancellation)
        {
            await _authenticationService.SignOutAsync(cancellation);
            return Ok();
        }

        
        [HttpPost("register")]
        [JwtAuthorize(Roles = "User")]
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


        [HttpPost("ForgotPassword")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<IActionResult> ForgotPassword(PasswordResetModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(Register), new { email = model.Email });
            }
            else if (user != null)
            {
                return RedirectToAction(nameof(ResetPassword), new { email = model.Email });
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action(
                action: nameof(ResetPassword),
                controller: "Account",
                values: new { token, email = user.Email },
                protocol: Request.Scheme
                );
            var emailSubject = "Reset";
            var emailBody = $"reset <a href = '{url}'>here</a>";
            await _emailSender.SendEmailAsync(user.Email, emailSubject, emailBody);
            return Ok(new {Message = " get it"});

        }

        [HttpPost("ResetPassword")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<IActionResult> ResetPassword(PasswordResetModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("not found");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
            }
            return Ok(new {Message = "reset good"}  );
        }

     
        [HttpPost("sendcode")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<IActionResult> SendConfirmCode(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("invalid");
            }   
            var confirmCode = new Random().Next(10000,99999).ToString();
            var code = new ConfirmCode
            {
                Email = email,
                Code = confirmCode,
                ExpiryDate = DateTime.UtcNow.AddMinutes(10),
            };
            _dbContext.ConfirmCodes.Add(code);
            await _dbContext.SaveChangesAsync();    
            return Ok("code was sended");
        }

        [HttpPost("verifycode")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<IActionResult> VerifyConfirmCode(string email, string code)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("error");
            }
            var codeEntry = await _dbContext.ConfirmCodes.FirstOrDefaultAsync(c => c.Email == email && c.Code == code);
            if (codeEntry == null)
            {
                return BadRequest("error code");
            }
            if (codeEntry.ExpiryDate< DateTime.UtcNow)
            {
                return BadRequest("time is out");
            }
            _dbContext.ConfirmCodes.Remove(codeEntry);
            await _dbContext.SaveChangesAsync();
            return Ok("matched");
            
        }

    }
}
