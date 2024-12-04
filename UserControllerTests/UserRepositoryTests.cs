using Microsoft.EntityFrameworkCore;
using UserServiceAPI.Application.Services.Common;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Infrastructure.DataBase;
using Xunit;

namespace UserAPITests
{
    public class UserRepositoryTests
    {
        private readonly MutableInnoShopDbContext _context;
        private readonly UserRepository _repository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<MutableInnoShopDbContext>()
                .UseNpgsql("UserTestDb")
                .Options;

            _context = new MutableInnoShopDbContext(options);
            _repository = new UserRepository(_context);
        }

        [Fact]
        public async Task GetAsync_UserExists_ReturnsUser()
        {
            // Arrange
            var user = new AppUsers { Id = 1, Name = "John Doe" };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAsync(user.Id, CancellationToken.None);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Equals(user.Id, result.Id);
        }
    }
}
