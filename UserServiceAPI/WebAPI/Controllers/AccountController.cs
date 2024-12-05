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
            try
            {
                var command = new LoginCommand(model);
                var token = await _mediator.Send(command, cancellation);
                return Ok(new { Token = token });
            }
            catch
            {
                return Unauthorized();
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
            await _mediator.Send(new LogoutCommand(), cancellation);
            return Ok();
        }
        /// <summary>
        /// Осуществление регистрации пользователя.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellation"></param>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationModel model, CancellationToken cancellation)
        {
            var command = new RegisterCommand(model);
            var result = await _mediator.Send(command, cancellation);
            if(result.Errors == null)
            {
                return BadRequest();
            }
            return Ok();
        }
        /// <summary>
        /// Осуществление функционала для восстановления пароля.
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("ForgotPassword")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<IActionResult> ForgotPassword([FromBody] PasswordResetModel model, CancellationToken cancellation)
        {
            var command = new ForgotPasswordCommand(model);
            var result = await _mediator.Send(command,cancellation);
            if (!result)
            {
                return BadRequest();    
            }
            return Ok();    
        }
        /// <summary>
        /// Осуществление отправки для подтверждения.
        /// </summary>
        /// <param name="email"></param>
        [HttpPost("sendcode")]
        [JwtAuthorize(Roles = "Admin, User")]
        public async Task<IActionResult> SendConfirmCode([FromQuery] string email, CancellationToken cancellation)
        {
            var command = new SendEmailCommand(email,"Confirm code", null);
            var result = await _mediator.Send(command, cancellation);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        /// <summary>
        /// Деактивация пользователя.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        [HttpPost("deactivateUser/{id}")]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> DeactivateUser([FromBody] int id, CancellationToken cancellation)
        {
            var command = new DeactivateUserCommand(id);
            var result = await _mediator.Send(command, cancellation);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

    }

}

