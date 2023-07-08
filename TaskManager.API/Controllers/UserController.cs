using Microsoft.AspNetCore.Mvc;
using TaskManager.DAL.DTO_s.User;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var response = await _userService.GetUserById(id.ToString());
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDto">The DTO containing the user data.</param>
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto userDto)
        {
            var response = await _userService.CreateUser(userDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Updates an existing user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="userDto">The DTO containing the updated user data.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserUpdateDto userDto)
        {
            var response = await _userService.UpdateUser(id.ToString(), userDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = await _userService.DeleteUser(id.ToString());
            return StatusCode((int)response.StatusCode, response);
        }
    }
}