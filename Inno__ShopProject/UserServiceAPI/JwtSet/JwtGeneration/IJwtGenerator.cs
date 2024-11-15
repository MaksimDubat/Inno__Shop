namespace UserServiceAPI.JwtSet.JwtGeneration
{
    /// <summary>
    /// Интерфейс сервиса генерации токенов.
    /// </summary>
    public interface IJwtGenerator
    {
        /// <summary>
        /// Создает и возращает токен доступа.
        /// </summary>
        /// <returns></returns>
        string GenerateToken();
    }
}
