using MediatR;
using UserServiceAPI.Domain.Entities;

namespace UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Commands
{
    /// <summary>
    /// Модель команды для обновления пользователя.
    /// </summary>
    public class UpdateUserCommand : IRequest<AppUsers>
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Сущность пользователя.
        /// </summary>
        public AppUsers User { get; set; }
        public UpdateUserCommand(int id, AppUsers user)
        {
            Id = id;
            User = user;
        }
    }
}
