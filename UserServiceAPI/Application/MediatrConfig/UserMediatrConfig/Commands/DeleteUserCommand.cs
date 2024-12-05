using MediatR;
using UserServiceAPI.Domain.Entities;

namespace UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Commands
{
    /// <summary>
    /// МОдель команды для удаления пользователя.
    /// </summary>
    public class DeleteUserCommand : IRequest<string>
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }
        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}
