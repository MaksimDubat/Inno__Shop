using MediatR;
using UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Commands;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;

namespace UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Handlres
{
    /// <summary>
    /// Обработчик команды для добавления пользователя.
    /// </summary>
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AppUsers>
    {
        private readonly IUserRepository _userRepository;
        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AppUsers> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.AddAsync(request.User, cancellationToken);
            return request.User;
        }
    }
}
