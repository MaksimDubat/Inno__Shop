namespace UserServiceAPI.Application.Models.PasswordResetModels
{
    /// <summary>
    /// Модель для восстановления пароля.
    /// </summary>
    public class PasswordResetModel
    {
        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Токен пользователя
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Новый пароль.
        /// </summary>
        public string NewPassword { get; set; }

    }
}
