﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserServiceAPI.Application.Models.LoginModels;
using UserServiceAPI.Application.Models.PasswordResetModels;
using UserServiceAPI.Application.Models.RegistartionModels;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;
using UserServiceAPI.Entities;
using UserServiceAPI.Infrastructure.DataBase;
using UserServiceAPI.JwtSet.JwtAttribute;

namespace UserServiceAPI.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с аккаунтом.
    /// </summary>
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
        /// <summary>
        /// Осуществление входа пользователя.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellation"></param>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model, CancellationToken cancellation)
        {
            try
            {
                var token = await _authenticationService.SignInAsync(model.Email, model.Password, cancellation);
                return Ok(new { Message = " log in god", Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid");
            }

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

    }
}