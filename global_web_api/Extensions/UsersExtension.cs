using global_web_api.Dtos;
using global_web_api.Interfaces;
using Newtonsoft.Json;

namespace global_web_api.Extensions
{
    /// <summary>
    /// User Extension class
    /// </summary>
    public static class UsersExtension
    {
        /// <summary>
        /// User extension method to get Users with GMAIL email address
        /// </summary>
        /// <param name="userService"></param>
        /// <returns></returns>
        public static async Task<List<UserDto>> GetUsersWithGmail(this IGeneric<UserDto> userService)
        {
            var allUsersString = await userService.GetAll();

            string jsonString;

            using (var streamReader = new StreamReader(await allUsersString.ReadAsStreamAsync()))
            {
                jsonString = await streamReader.ReadToEndAsync();

            }

            List<UserDto> allUsers = JsonConvert.DeserializeObject<List<UserDto>>(jsonString);
            List<UserDto> gmailUsers = allUsers.Where(user => user.Email.Contains("gmail", StringComparison.CurrentCultureIgnoreCase)).ToList();

            return gmailUsers;
        }
    }
}
