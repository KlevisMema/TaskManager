/*
    This interface defines the contract for managing users.
    It provides CRUD operations and business logic for user management, including soft delete, hard delete, and restore.

    - GetUsers: Retrieves a list of all users.
    - GetUserById: Retrieves a user by their ID.
    - CreateUser: Creates a new user.
    - UpdateUser: Updates an existing user.
    - SoftDeleteUser: Soft deletes a user by their ID.
    - HardDeleteUser: Hard deletes a user by their ID.
    - RestoreUser: Restores a soft-deleted user by their ID.
*/

#region Usings
using TaskManager.DTO.DTO_s.User;
using TaskManager.HELPERS.ServiceResponse;
#endregion

namespace TaskManager.BLL.RepositoryPattern.ServicesInterfaces
{
    /// <summary>
    /// This interface defines the contract for managing users.
    /// </summary>
    public interface IUserService
    {
        #region Methods

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing a list of user DTOs.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving users.</exception>
        Task<Response<List<UserDto>>> 
        GetUsers();

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the user DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the user.</exception>
        Task<Response<UserDto>> 
        GetUserById
        (
            string userId
        );

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDto">The DTO containing the user data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the created user DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while creating the user.</exception>
        Task<Response<UserDto>> 
        CreateUser
        (
            UserCreateDto userDto
        );

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="userDto">The DTO containing the updated user data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the updated user DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the user.</exception>
        Task<Response<UserDto>> 
        UpdateUser
        (
            string userId, 
            UserUpdateDto userDto
        );

        /// <summary>
        /// Soft deletes a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to soft delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing a boolean indicating if the soft deletion was successful.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while soft deleting the user.</exception>
        Task<Response<bool>> 
        SoftDeleteUser
        (
            string userId
        );

        /// <summary>
        /// Hard deletes a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to hard delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing a boolean indicating if the hard deletion was successful.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while hard deleting the user.</exception>
        Task<Response<bool>> 
        HardDeleteUser
        (
            string userId
        );

        /// <summary>
        /// Restores a soft-deleted user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to restore.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing a boolean indicating if the restore operation was successful.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while restoring the user.</exception>
        Task<Response<bool>> 
        RestoreSoftDeletedUser
        (
            string userId
        ); 

        #endregion
    }
}