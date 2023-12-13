using global_web_api.Dtos;
using global_web_api.Models;
using Newtonsoft.Json;
using System.Text;

namespace web_api_test.MockData
{
    public class MockUserData
    {
        public static List<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User
                {
                    Id=1,
                    Name = "Samson Kim",
                    Email="SamKim@yahoo.com",
                    PasswordHash="uyeugjhkje6873980879687683798jkjvhknvkfiouifhk",
                    CreatedAt=DateTime.Now,
                    updatedAt = DateTime.Now,
                },
                new User
                {
                    Id=2,
                    Name = "Yemi Lot",
                    Email="yemilot@yahoo.com",
                    PasswordHash="uyeugjhkje68739dfdfkd80879687683798jkjvhknhhkvkfiouifhk",
                    CreatedAt=DateTime.Now,
                    updatedAt = DateTime.Now,
                },
                new User
                {
                    Id=3,
                    Name = "Paul Kane",
                    Email="KanePaul@yahoo.com",
                    PasswordHash="5353ufjopyeugjhkje6873980879687683798jkjvhknvkfiouifhk",
                    CreatedAt=DateTime.Now,
                    updatedAt = DateTime.Now,
                }
            };



            return users;

        }

        public static UserDto AddUser()
        {
            var user = new UserDto()
            {
                Name = "Susan Harry",
                Email = "talk2Sussy@yahoo.com",
                Password = "sammy123#"
            };
            return user;
        }

        public static List<UserDto> EmptyUsers()
        {
            return new List<UserDto>() { };
        }


        /// <summary>
        /// Picks a random User from the Users list and return his Id
        /// </summary>
        /// <returns></returns>
        public static int UserId()
        {
            // Create an instance of Random
            Random random = new Random();

            // Generate a random integer within the specified range
            int randomNumber = random.Next(0, GetUsers().Count);

            return GetUsers().ElementAt(randomNumber).Id;

        }

        public static async Task<HttpContent> GetUsersAsContent()
        {
            var allUsers = GetUsers();
            var allUsersDto = allUsers.Select(u => new UserDto { Name = u.Name!, Email = u.Email, Password = u.PasswordHash });
            //Serialize
            var jsonString = JsonConvert.SerializeObject(allUsersDto);


            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }
    }
}
