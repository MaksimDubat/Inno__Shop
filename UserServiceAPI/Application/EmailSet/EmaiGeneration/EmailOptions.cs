namespace UserServiceAPI.Application.EmailSet.EmaiGeneration
{
    /// <summary>
    /// Класс элементов, необходимых для отправки Email через SMTP-сервер.
    /// </summary>
    public class EmailOptions
    {
        /// <summary>
        /// Email отправителя.
        /// </summary>
        public string From { get; set; } = string.Empty;
        /// <summary>
        /// Адрес SMTP-сервера.
        /// </summary>
        public string SmtpServer { get; set; } = string.Empty;
        /// <summary>
        /// Порт подключения.
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
