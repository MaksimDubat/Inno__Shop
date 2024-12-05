using MediatR;
using UserServiceAPI.Domain.Entities;

namespace UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Queries
{
    /// <summary>
    /// Модель запроса на получение пользователя по Email.
    /// </summary>
    public class GetUserByEmailQuery : IRequest<AppUsers>
    {
        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string Email { get; set; }
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
