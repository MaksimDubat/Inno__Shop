using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using UserServiceAPI.Application.JwtSet.JwtAttribute;
using UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands;
using UserServiceAPI.Application.Models.LoginModels;
using UserServiceAPI.Application.Models.PasswordResetModels;
using UserServiceAPI.Application.Models.RegistartionModels;
using UserServiceAPI.Application.Services.Common;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;
using UserServiceAPI.Infrastructure.DataBase;

namespace UserServiceAPI.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с аккаунтом.
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Осуществление входа пользователя.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellation"></param>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model, CancellationToken cancellation)
        {
            var token = await _mediator.Send(new LoginCommand(model.Email, model.Password), cancellation);
            if(token.IsNullOrEmpty())
            {
                return Unauthorized();
            }
            return Ok(new { Message = "Login ok", token });
        }
        /// <summary>
        /// Осуществление выхода пользователя.
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellation)
        {
            await _authenticationService.SignOutAsync(cancellation);
            return Ok("logout");
        }
        /// <summary>
        /// Осуществление регистрации пользователя.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellation"></param>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationModel model, CancellationToken cancellation)
        {
            var result = await _authenticationService.RegisterAsync(model.Email, model.Name, model.Password, cancellation);
            if (result.Succeeded)
            {
                var token = await _authenticationService.SignInAsync(model.Email, model.Password, cancellation);
                return Ok(new { Message = " reg god", Token = token });
            }
            else
            {
                var errors = result.Errors?.Select(x => x.Description).ToList();
                return BadRequest(errors);
            }
        }
        /// <summary>
        /// Осуществление функционала при потери пароля.
        /// </summary>
        /// <param name="model"></param>
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
            return Ok(new { Message = " get it" });

        }
        /// <summary>
        /// Осуществление функции восстановления пароля.
        /// </summary>
        /// <param name="model"></param>
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
            return Ok(new { Message = "reset good" });
        }
        /// <summary>
        /// Осуществление отправки для подтверждения.
        /// </summary>
        /// <param name="email"></param>
        [HttpPost("sendcode")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<IActionResult> SendConfirmCode(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("invalid");
            }
            var confirmCode = new Random().Next(10000, 99999).ToString();
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
        /// <summary>
        /// Осуществление проверки кода.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
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
            if (codeEntry.ExpiryDate < DateTime.UtcNow)
            {
                return BadRequest("time is out");
            }
            _dbContext.ConfirmCodes.Remove(codeEntry);
            await _dbContext.SaveChangesAsync();
            return Ok("matched");

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
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("deactivation error");
            }
            return Ok("diactivation ok");
        }

    }

}

