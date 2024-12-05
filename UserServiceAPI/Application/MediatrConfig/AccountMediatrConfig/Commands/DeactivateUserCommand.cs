using MediatR;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands
{
    /// <summary>
    /// Модель команды деактивации пользователя.
    /// </summary>
    public class DeactivateUserCommand : IRequest<bool>
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UsertId { get; set; }   
        public DeactivateUserCommand(int usertId)
        {
            UsertId = usertId;
        }
    }
}
