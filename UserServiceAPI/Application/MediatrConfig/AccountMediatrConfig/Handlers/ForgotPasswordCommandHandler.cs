using MediatR;
using UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands;
using UserServiceAPI.Domain.Interface;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Handlers
{
    /// <summary>
    /// Обработчик для сброса пароля пользователя.
    /// </summary>
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IAuthenticationService _authenticationService;
        public ForgotPasswordCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var resetModel = request.Model;
            return await _authenticationService.ResetPasswordAsync(
                resetModel.Email,
                resetModel.Token,
                resetModel.NewPassword,
                cancellationToken);
        }
    }
}
