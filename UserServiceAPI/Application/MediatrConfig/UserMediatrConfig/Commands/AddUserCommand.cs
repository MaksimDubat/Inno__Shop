using MediatR;
using UserServiceAPI.Domain.Entities;

namespace UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Commands
{
    /// <summary>
    /// Модель команды для добавления пользователя.
    /// </summary>
    public class AddUserCommand : IRequest<AppUsers>
    {
        /// <summary>
        /// Сущность пользователя.
        /// </summary>
        public AppUsers User {  get; set; }

        public AddUserCommand(AppUsers user)
        {
            User = user;
        }
    }
}
