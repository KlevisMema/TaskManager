/*
    This service class provides CRUD operations and business logic for managing users.
    It interacts with the database through the application's DbContext and uses AutoMapper for mapping between DTOs and entities.

    - GetUsers: Retrieves a list of all users.
    - CreateUser: Creates a new user.
    - GetUserById: Retrieves a user by their ID.
    - UpdateUser: Updates an existing user.
    - SoftDeleteUser: Soft deletes a user by their ID.
    - HardDeleteUser: Hard deletes a user by their ID.
    - RestoreSoftDeletedUser: Restores a soft-deleted user by their ID.
*/


#region Usings
using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.Context;
using TaskManager.DTO.DTO_s.User;
using TaskManager.BLL.BaseServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.HELPERS.LogsHelper;
using TaskManager.HELPERS.ServiceResponse;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;
#endregion

namespace TaskManager.BLL.RepositoryPattern.Services
{
    /// <summary>
    /// Service class for managing users.
    /// </summary>
    public class UserService : BaseService, IUserService
    {

        #region Fields
        /// <summary>
        /// The user manager instance provided by ASP.NET Core Identity.
        /// </summary>
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="userManager">The UserManager instance.</param>
        /// <param name="dbContext">The application's database context.</param>
        public UserService
        (
            IMapper mapper,
            UserManager<User> userManager,
            ApplicationDbContext dbContext
        ) : base(mapper, dbContext)
        {
            _userManager = userManager;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing a list of user DTOs.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving users.</exception>
        public async Task<Response<List<UserDto>>>
        GetUsers()
        {
            try
            {
                var users = await _dbContext.Users.ToListAsync();
                return Response<List<UserDto>>.Ok(_mapper.Map<List<UserDto>>(users), "Users retrieved successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);
                return Response<List<UserDto>>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDto">The DTO containing the user data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the created user DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while creating the user.</exception>
        public async Task<Response<UserDto>>
        CreateUser
        (
            UserCreateDto userDto
        )
        {
            try
            {
                var user = _mapper.Map<User>(userDto);

                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (!result.Succeeded)
                    return Response<UserDto>.UnSuccessMessage(result.Errors.First().Description);

                var createdUser = await _userManager.FindByEmailAsync(userDto.Email);

                return Response<UserDto>.Ok(_mapper.Map<UserDto>(createdUser), "User created successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<UserDto>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the user DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the user.</exception>
        public async Task<Response<UserDto>>
        GetUserById
        (
            string userId
        )
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return Response<UserDto>.NotFound($"User with id: {userId} doesn't exist.");

                return Response<UserDto>.Ok(_mapper.Map<UserDto>(user), $"User {user.UserName} retrieved successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<UserDto>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="userDto">The DTO containing the updated user data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the updated user DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the user.</exception>
        public async Task<Response<UserDto>>
        UpdateUser
        (
            string userId,
            UserUpdateDto userDto
        )
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return Response<UserDto>.NotFound($"User with id: {userId} doesn't exist.");

                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return Response<UserDto>.UnSuccessMessage(result.Errors.First().Description);

                return Response<UserDto>.Ok(_mapper.Map<UserDto>(user), $"User with id: {userId} updated successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<UserDto>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Soft deletes a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to soft delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing a boolean indicating if the soft deletion was successful.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while soft deleting the user.</exception>
        public async Task<Response<bool>>
        SoftDeleteUser
        (
            string userId
        )
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return Response<bool>.NotFound($"User with id: {userId} doesn't exist.");

                user.IsDeleted = true;
                user.DeletedAt = DateTime.UtcNow;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return Response<bool>.UnSuccessMessage(result.Errors.First().Description);

                return Response<bool>.Ok(true, $"User with id: {userId} soft deleted successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Restores a soft-deleted user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to restore.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing a boolean indicating if the restore operation was successful.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while restoring the user.</exception>
        public async Task<Response<bool>>
        RestoreSoftDeletedUser
        (
            string userId
        )
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return Response<bool>.NotFound($"User with id: {userId} doesn't exist.");

                user.IsDeleted = false;
                user.DeletedAt = null;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return Response<bool>.UnSuccessMessage(result.Errors.First().Description);

                return Response<bool>.Ok(true, $"User with id: {userId} restored successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Hard deletes a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to hard delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing a boolean indicating if the hard deletion was successful.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while hard deleting the user.</exception>
        public async Task<Response<bool>>
        HardDeleteUser
        (
            string userId
        )
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return Response<bool>.NotFound($"User with id: {userId} doesn't exist.");

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    return Response<bool>.UnSuccessMessage(result.Errors.First().Description);

                return Response<bool>.Ok(true, $"User with id: {userId} hard deleted successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        } 

        #endregion

    }
}