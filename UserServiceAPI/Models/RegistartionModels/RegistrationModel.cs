using System.ComponentModel.DataAnnotations;

namespace UserServiceAPI.Models.RegistartionModels
{
    /// <summary>
    /// Модель для регистрации пользователя.
    /// </summary>
    public class RegistrationModel
    {
        /// <summary>
        /// Email пользователя.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// Подтверждение пароля пользователя.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// Список ошибок.
        /// </summary>
        public List<string>? Errors { get; set; }
    }
}
