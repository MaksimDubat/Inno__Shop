using MediatR;
using UserServiceAPI.Domain.Entities;

namespace UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Queries
{
    /// <summary>
    /// Модель запроса на получение пользователя по идентификатору.
    /// </summary>
    public class GetUserByIdQuery : IRequest<AppUsers>
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
