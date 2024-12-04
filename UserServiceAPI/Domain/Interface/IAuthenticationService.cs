using Microsoft.AspNetCore.Identity;

namespace UserServiceAPI.Domain.Interface
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
        Task<string> SignInAsync(string email, string password, CancellationToken cancellation);

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
        Task<IdentityResult> RegisterAsync(string email, string name, string password, CancellationToken cancellation);
    }
}
