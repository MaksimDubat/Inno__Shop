using UserServiceAPI.Entities;

namespace UserServiceAPI.Interface
{
    /// <summary>
    /// Репозиторий по работе с пользователем.
    /// </summary>
    public interface IUserRepository : IRepository<AppUsers>
    {
        Task<AppUsers> GetUserByEmailAsync (string email, CancellationToken cancellation );
    }
}
