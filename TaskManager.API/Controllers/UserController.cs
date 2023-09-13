/*
 This file contains the UserController class which provides endpoints for managing users in the TaskManager API.
 It includes functionalities such as retrieving all users, retrieving a user by their ID, creating a new user, and updating an existing user.
*/

#region Usings
using Microsoft.AspNetCore.Mvc;
using TaskManager.DTO.DTO_s.User;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces; 
#endregion

namespace TaskManager.API.Controllers
{
    /// <summary>
    /// UserController is a controller that provides endpoints for managing users.
    /// It allows to get all users, get a user by id, create a new user and update an existing user.
    /// </summary>
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor for UserController.
        /// </summary>
        /// <param name="userService">Service for user operations.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
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
        /// <returns>The user with the given ID.</returns>
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
        /// <returns>The created user.</returns>
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
        /// <returns>The updated user.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserUpdateDto userDto)
        {
            var response = await _userService.UpdateUser(id.ToString(), userDto);
            return StatusCode((int)response.StatusCode, response);
        }

    }
}