using MediatR;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands
{
    /// <summary>
    /// Модель комнады для выхода пользователя. 
    /// </summary>
    public class LogoutCommand : IRequest<Unit>
    {
    }
}
