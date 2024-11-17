namespace UserServiceAPI.Entities
{
    /// <summary>
    /// Сущность для работы над результатами аутентификации (JWT).
    /// </summary>
    public class LoginResponse
    {
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string UserId { get; set; }
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
