using UserServiceAPI.Entities;

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
        string GenerateToken(AppUsers user, IList<string> roles);
    }
}
