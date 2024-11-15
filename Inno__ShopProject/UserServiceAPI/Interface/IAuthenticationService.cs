﻿using Microsoft.AspNetCore.Identity;
using UserServiceAPI.Infrastructure.Common;

namespace UserServiceAPI.Interface
{
    /// <summary>
    /// Интерфейс сервиса аунтефикации.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Осуществляет вход пользователя.
        /// </summary>
        /// <param name="email">Email пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task<bool> SignInAsync(string email, string password, CancellationToken cancellation);

        /// <summary>
        /// Осуществляет выход пользователя.
        /// </summary>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task SignOutAsync(CancellationToken cancellation);

        /// <summary>
        /// Осуществляет регистрацию пользователя.
        /// </summary>
        /// <param name="email">Email пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task<IdentityResult> RegisterAsync(string name, string email, string password, CancellationToken cancellation);
    }
}
