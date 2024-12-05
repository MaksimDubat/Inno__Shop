using MediatR;
using UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Commands;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;

namespace UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Handlres
{
    /// <summary>
    /// Обработчик запроса для удаления пользователя.
    /// </summary>
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(request.Id, cancellationToken);
            return $"{request.Id} deleted.";
        }
    }
}
