using UserServiceAPI.Domain.Entities;

namespace UserServiceAPI.Domain.Interface
{
    /// <summary>
    /// Репозиторий по работе с пользователем.
    /// </summary>
    public interface IUserRepository : IRepository<AppUsers>
    {
        Task<AppUsers> GetUserByEmailAsync(string email, CancellationToken cancellation);
    }
}
