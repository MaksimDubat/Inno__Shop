namespace UserServiceAPI.EmailSet.EmaiGeneration
{
    public class EmailOptions
    {
        public string From { get; set; } = string.Empty;
        public string SmtpServer { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
