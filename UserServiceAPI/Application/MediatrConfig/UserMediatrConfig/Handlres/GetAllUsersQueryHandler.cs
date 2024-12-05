using MediatR;
using UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Queries;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;

namespace UserServiceAPI.Application.MediatrConfig.UserMediatrConfig.Handlres
{
    /// <summary>
    /// Обработчик запроса на получение всех пользователей.
    /// </summary>
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<AppUsers>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<AppUsers>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllAsync(cancellationToken);
        }
    }
}
