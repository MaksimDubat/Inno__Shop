﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;
using UserServiceAPI.WebAPI.Controllers;
using Xunit;

namespace UserAPITests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UsersController _controller;

        public UserControllerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _controller = new UsersController(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetById_UserExists_ReturnsOkResult()
        {
            // Arrange
            var userId = 1;
            var user = new AppUsers { Id = userId, Name = "John Doe" };
            _userRepositoryMock.Setup(repo => repo.GetAsync(userId, It.IsAny<CancellationToken>()))
                               .ReturnsAsync(user);

            // Act
            var result = await _controller.GetUserById(userId, CancellationToken.None);

            // Assert
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Xunit.Assert.IsType<AppUsers>(okResult.Value);  
            Xunit.Assert.Equal(userId, returnedUser.Id);  
        }

        [Fact]
        public async Task GetById_UserDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var userId = 1;
            _userRepositoryMock.Setup(repo => repo.GetAsync(userId, It.IsAny<CancellationToken>()))
                               .ReturnsAsync((AppUsers)null);

            // Act
            var result = await _controller.GetUserById(userId, CancellationToken.None);

            // Assert
            Xunit.Assert.IsType<NotFoundResult>(result.Result); 
        }
    }
}
