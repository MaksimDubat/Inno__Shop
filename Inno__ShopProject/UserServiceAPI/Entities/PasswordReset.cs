namespace UserServiceAPI.Entities
{
    /// <summary>
    /// Модель для восстановления пароля.
    /// </summary>
    public class PasswordReset
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Токен пользователя
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Дата действия токена.
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}
