using global_web_api.Dtos;
using global_web_api.Extensions;
using global_web_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace global_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Get the IUser Service
        private readonly IGeneric<UserDto> _userService;

        public UserController(IGeneric<UserDto> userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get All Users Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet("/users")]
        public async Task<IActionResult> GetUsers()
        {
            var allUsersString = await _userService.GetAll();

            string jsonString;
            using (var streamReader = new StreamReader(await allUsersString.ReadAsStreamAsync()))
            {
                jsonString = await streamReader.ReadToEndAsync();
            };
            List<UserDto> users = JsonConvert.DeserializeObject<List<UserDto>>(jsonString);
            if (users!.Count != 0)
            {
                return Ok(users);
            }

            return Ok("No Users");
        }


        /// <summary>
        /// Add new User ned point
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("/users")]
        public async Task<IActionResult> AddUser([FromBody] UserDto user)
        {
            var result = await _userService.Add(user);
            return Ok("User Successfully Added");
        }

        /// <summary>
        /// Endpoint to Update selected user 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPut("/users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto user)
        {
            var result = await _userService.Update(id, user);
            return Ok("User Data has been Updated");
        }


        /// <summary>
        /// Endpoint to Delete selected user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.Delete(id);
            return Ok(result);
        }


        /// <summary>
        /// End point to get specified user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/users/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _userService.GetItem(id);
            return Ok("User Sucessfully Deleted");
        }


        /// <summary>
        /// Endpoint to get users with GMAIL email address
        /// </summary>
        /// <returns></returns>
        [HttpGet("/get-gmail-users")]
        public async Task<List<UserDto>> GetGmailUsers()
        {
            return await _userService.GetUsersWithGmail();
        }
    }
}
