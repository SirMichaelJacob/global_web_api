using global_web_api.Context;
using global_web_api.Dtos;
using global_web_api.Interfaces;
using global_web_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using System.Text.Json;

namespace global_web_api.Services
{
    /// <summary>
    /// A CRUD Service class dependent on a generic Interface
    /// </summary>
    public class UserService : IGeneric<UserDto>
    {
        private readonly MyDbContext _dbContext;
        private readonly PasswordHasher<UserDto> passwordHasher;
        public UserService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
            passwordHasher = new PasswordHasher<UserDto>();
        }

        /// <summary>
        /// Async method for adding new User to Database.
        /// Returns a Http Status code
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Add(UserDto user)
        {

            User newUser = new User
            {
                Name = user.Name,
                Email = user.Email!.ToLower(),
                PasswordHash = passwordHasher.HashPassword(user, user.Password),
            };

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            var response = new HttpResponseMessage(HttpStatusCode.Created);

            return response;

        }

        /// <summary>
        /// Async method for User delete operation from Database.
        /// Returns a Http Status code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Delete(int id)
        {
            User user = await _dbContext!.Users!.FindAsync(id);

            _dbContext.Users.Remove(user!);
            await _dbContext.SaveChangesAsync();
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            return response;


        }

        /// <summary>
        /// Async method for Retrieving Users from Database.
        /// Returns a Serialized Json String 
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> GetAll()
        {
            var Users = await _dbContext.Users.Select(q => new UserDto { Name = q.Name!, Email = q.Email, Password = q.PasswordHash }).ToListAsync();

            // Serialize the list of ProductDto objects
            var json = JsonSerializer.Serialize(Users); // or: var json = JsonConvert.SerializeObject(allProductsDto);

            // Prepare the HttpResponseMessage
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return response.Content;
        }

        /// <summary>
        /// Async method for Retrieving User from Database.
        /// Returns User Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto> GetItem(int id)
        {
            User user = await _dbContext.Users.SingleAsync(q => q.Id == id);

            var userData = new UserDto { Name = user.Name, Email = user.Email, Password = user.PasswordHash };
            return userData;
        }

        /// <summary>
        /// Async method for Updating User details in Database.
        /// Returns a Http Status code        
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Update(int id, UserDto userData)
        {
            var user = await _dbContext.Users.FindAsync(id);



            var password = "";
            user.Name = userData.Name;
            user.Email = userData.Email!;

            if (userData.Password.Length > 0)
            {
                password = passwordHasher.HashPassword(userData, userData.Password);

            }
            else
            {
                password = user.PasswordHash;
            }

            user.PasswordHash = password;
            user.updatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            var response = new HttpResponseMessage(HttpStatusCode.OK);

            return response;

        }


    }
}
