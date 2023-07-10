/*
    This file contains the interface IOAuthJwtTokenService, which defines the contract for creating JWT tokens for authentication.
*/

using TaskManager.DAL.DTO_s.User;

namespace TaskManagment.SECURITY.JWTAuthenticationService.ServiceInterface
{
    /// <summary>
    /// Interface for creating JWT tokens for authentication.
    /// </summary>
    public interface IOAuthJwtTokenService
    {
        /// <summary>
        /// Creates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user information. <see cref="UserDto"/></param>
        /// <returns>The generated JWT token.</returns>
        string
        CreateToken
        (
            UserDto user
        );
    }
}