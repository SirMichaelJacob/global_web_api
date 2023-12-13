using global_web_api.Controllers;
using global_web_api.Dtos;
using global_web_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using web_api_test.MockData;

namespace web_api_test.System.Controllers
{
    public class TestUserController
    {
        [Fact]
        public async Task GetAllUsers_ShouldReturnStatusCode200()
        {
            //Arrange
            var _userService = new Mock<IGeneric<UserDto>>();
            _userService.Setup(x => x.GetAll()).Returns(MockUserData.GetUsersAsContent());

            //Act
            var sut = new UserController(_userService.Object);

            var result = await sut.GetUsers();

            //Assert
            Assert.True(result.GetType() == typeof(OkObjectResult));
        }

        [Fact]
        public async Task AddNewUser_ReturnsOK()
        {
            //Arrange
            var _userService = new Mock<IGeneric<UserDto>>();
            _userService.Setup(x => x.GetAll()).Returns(MockUserData.GetUsersAsContent());

            //Act
            var sut = new UserController(_userService.Object);
            var result = await sut.AddUser(MockUserData.AddUser());

            //Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task DeleteUser_CheckReturnObject()
        {
            //Arrange
            var _userService = new Mock<IGeneric<UserDto>>();
            _userService.Setup(x => x.GetAll()).Returns(MockUserData.GetUsersAsContent());

            //Act
            var sut = new UserController(_userService.Object);
            var userId = MockUserData.UserId();
            var result = await sut.DeleteUser(userId);
            //Arrange
            Assert.NotNull(result);
            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));
        }

        [Fact]
        public async Task UpdateUser_CheckReturnObject()
        {
            //Arrange
            var _userService = new Mock<IGeneric<UserDto>>();
            _userService.Setup(x => x.GetAll()).Returns(MockUserData.GetUsersAsContent());

            //Act
            var sut = new UserController(_userService.Object);
            var userId = MockUserData.UserId();
            var result = await sut.UpdateUser(userId, MockUserData.AddUser());

            //Arrange
            Assert.NotNull(result);
            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));
        }

        [Fact]
        public async Task GetUser_CheckReturnObject()
        {
            //Arrange
            var _userService = new Mock<IGeneric<UserDto>>();
            _userService.Setup(x => x.GetAll()).Returns(MockUserData.GetUsersAsContent());

            //Act
            var sut = new UserController(_userService.Object);
            var userId = MockUserData.UserId();
            var result = await sut.GetUser(userId);

            //Arrange
            Assert.NotNull(result);
            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));

        }

    }
}
