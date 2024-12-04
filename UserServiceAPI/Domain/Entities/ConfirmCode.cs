namespace UserServiceAPI.Domain.Entities
{
    public class ConfirmCode
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
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
