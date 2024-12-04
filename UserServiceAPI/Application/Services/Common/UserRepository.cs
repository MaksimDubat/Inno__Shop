using Microsoft.EntityFrameworkCore;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;
using UserServiceAPI.Infrastructure.DataBase;

namespace UserServiceAPI.Application.Services.Common
{
    /// <summary>
    /// Репозиторий для работы с пользователями, расширяющий базовый репозиторий.
    /// </summary>
    public class UserRepository : EfRepositoryBase<AppUsers>, IUserRepository
    {
        private readonly MutableInnoShopDbContext _context;
        public UserRepository(MutableInnoShopDbContext mutableDbContext) : base(mutableDbContext)
        {
            _context = mutableDbContext;
        }

        public async Task<AppUsers> GetUserByEmailAsync(string email, CancellationToken cancellation)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == email, cancellation);

        }
    }
}
