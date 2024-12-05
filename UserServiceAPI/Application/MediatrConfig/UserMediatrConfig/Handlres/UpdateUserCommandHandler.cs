using MediatR;
using UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Commands;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;

namespace UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Handlres
{
    /// <summary>
    /// Обработчик запроса для обновления пользователя.
    /// </summary>
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, AppUsers>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<AppUsers> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if(request.Id != request.User.Id)
            {
                throw new ArgumentException();
            }
            await _userRepository.UpdateAsync(request.User, cancellationToken);
            return request.User;
        }
    }
}
