using Microsoft.EntityFrameworkCore;
using UserServiceAPI.DataBaseAccess;
using UserServiceAPI.Entities;
using UserServiceAPI.Interface;

namespace UserServiceAPI.Infrastructure.Common
{
    public class UserRepository : EfRepositoryBase<AppUsers>, IUserRepository
    {
        private readonly MutableInnoShopDbContext _context;
        public UserRepository(MutableInnoShopDbContext mutableDbContext) : base(mutableDbContext)
        {
            _context = mutableDbContext;
        }

        public async Task<AppUsers> GetUserByEmailAsync(string email, CancellationToken cancellation)
        {
           return await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == email);
           
        }
    }
}
