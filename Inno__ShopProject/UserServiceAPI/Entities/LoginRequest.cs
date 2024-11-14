namespace UserServiceAPI.Entities
{
    /// <summary>
    /// Модель для работы над аутентификацией пользователя.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Почта пользователя.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }
    }
}
