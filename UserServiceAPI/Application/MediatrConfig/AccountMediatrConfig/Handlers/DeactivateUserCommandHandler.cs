using MediatR;
using Microsoft.AspNetCore.Identity;
using UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Handlers
{
    /// <summary>
    /// Обработчик команды деактивации пользователя.
    /// </summary>
    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, bool>
    {
        private readonly IUserRepository _repository;
        public DeactivateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync(request.UsertId, cancellationToken);
            if (user == null)
            {
                return false;
            }
            user.IsActive = false;
            await _repository.UpdateAsync(user, cancellationToken);
            return true;
        }
    }
}
