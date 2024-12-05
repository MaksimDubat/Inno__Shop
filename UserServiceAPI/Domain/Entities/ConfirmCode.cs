namespace UserServiceAPI.Domain.Entities
{
    public class ConfirmCode
    { 
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Код
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime ExpiryDate { get; set; }
    }
}
