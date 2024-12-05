using MediatR;
using UserServiceAPI.Domain.Entities;

namespace UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Queries
{
    /// <summary>
    /// Модель запроса для получения всех пользователей.
    /// </summary>
    public class GetAllUsersQuery : IRequest<IEnumerable<AppUsers>>
    {
    }
}
