using FluentAssertions;
using global_web_api.Context;
using global_web_api.Dtos;
using global_web_api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using web_api_test.MockData;

namespace web_api_test.System.Services
{

    public class TestUserService : IDisposable
    {
        private readonly MyDbContext _dbContext;
        private readonly PasswordHasher<UserDto> passwordHasher;
        public TestUserService()
        {
            passwordHasher = new PasswordHasher<UserDto>();
            var options = new DbContextOptionsBuilder<MyDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _dbContext = new MyDbContext(options);
            _dbContext.Database.EnsureCreated();

        }

        [Fact]
        public async Task TestUserService_AddUser()
        {


            //Arrange
            await _dbContext.Users.AddRangeAsync(MockUserData.GetUsers());
            await _dbContext.SaveChangesAsync();


            //Act
            var sut = new UserService(_dbContext);
            var result = sut.Add(MockUserData.AddUser());
            //Assert

            int expectedLength = MockUserData.GetUsers().Count + 1;

            var allUsers = await _dbContext.Users.ToListAsync();
            allUsers.Count.Should().Be(expectedLength);
        }

        [Fact]
        public async Task DeleteUser_CountUsersInDb()
        {
            // Arrange
            await _dbContext.Users.AddRangeAsync(MockUserData.GetUsers());
            await _dbContext.SaveChangesAsync();

            var sut = new UserService(_dbContext);

            // Act
            await sut.Delete(MockUserData.UserId());

            // Assert
            var allUsers = await _dbContext.Users.ToListAsync();
            Assert.True(allUsers.Count == 2);
        }

        [Fact]
        public async Task Update_CheckUserDetails()
        {
            //Arrange
            await _dbContext.AddRangeAsync(MockUserData.GetUsers());
            await _dbContext.SaveChangesAsync();


            var newUser = new UserDto
            {
                Name = MockUserData.AddUser().Name,
                Email = MockUserData.AddUser().Email!,
                //PasswordHash = "676874hfjkjdmioiyirjmvnv8748fjkmndr8fnmlkel67238"
                Password = passwordHasher.HashPassword(MockUserData.AddUser(), MockUserData.AddUser().Password)
            };

            int userId = MockUserData.UserId();
            //Act
            var sut = new UserService(_dbContext);
            var result = sut.Update(userId, newUser);
            //Assert
            var UpdatedUser = await _dbContext.Users.FindAsync(userId);

            Assert.True(UpdatedUser!.Name == newUser.Name);
            Assert.True(UpdatedUser!.Email == newUser.Email);

        }

        [Fact]

        public async Task GetUserById_CompareUserData()
        {
            //Arrange
            await _dbContext.AddRangeAsync(MockUserData.GetUsers());
            await _dbContext.SaveChangesAsync();

            int userId = MockUserData.UserId();

            //Act
            var sut = new UserService(_dbContext);
            var result = await sut.GetItem(userId);
            var users = new List<UserDto>
            {
                result
            };
            //Assert
            Assert.Single(users);
            Assert.True(result.Name == MockUserData.GetUsers().Find(x => x.Id == userId)!.Name);

        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
