namespace UserServiceAPI.Entities
{
    /// <summary>
    /// Сущность для работы над результатами аутентификации (JWT).
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Токен пользователя.
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Время истечения токена.
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}
