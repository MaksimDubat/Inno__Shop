namespace UserServiceAPI.Application.Services.Common
{
    /// <summary>
    /// Перечисление для разделения ролей пользователя.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Администратор.
        /// </summary>
        Admin,
        /// <summary>
        /// Пользователь.
        /// </summary>
        User,
        /// <summary>
        /// Гость.
        /// </summary>
        Guest
    }
}
