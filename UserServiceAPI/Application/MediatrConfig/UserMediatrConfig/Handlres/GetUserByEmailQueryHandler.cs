using MediatR;
using UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Queries;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;

namespace UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Handlres
{
    /// <summary>
    /// Обработчик запроса на получение пользователя по Email. 
    /// </summary>
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, AppUsers>
    {
        public IUserRepository _userRepository;
        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AppUsers> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user == null)
            {
                new ArgumentNullException();
            }
            return user;
        }
    }
}
