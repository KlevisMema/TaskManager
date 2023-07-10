/*

This file contains the implementation of the IOAuthJwtTokenService interface which is responsible for creating JWT tokens for authentication.

*/

#region Usings
using System.Text;
using System.Security.Claims;
using TaskManager.DTO.DTO_s.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TaskManagment.SECURITY.JWTAuthenticationService.ServiceInterface;
#endregion

namespace TaskManagment.SECURITY.JWTAuthenticationService.ServiceImplementation
{
    /// <summary>
    /// Service for creating JWT tokens for authentication.
    /// </summary>
    public class OAuthJwtTokenService : IOAuthJwtTokenService
    {
        /// <summary>
        /// Represents the JWT authentication options.
        /// </summary>
        private readonly IOptions<AuthenticationSettings> _jwtOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthJwtTokenService"/> class.
        /// </summary>
        /// <param name="jwtOptions">The JWT authentication options.</param>
        public OAuthJwtTokenService
        (
            IOptions<AuthenticationSettings> jwtOptions
        )
        {
            _jwtOptions = jwtOptions;
        }

        /// <summary>
        /// Creates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user information. <see cref="UserDto"/></param>
        /// <returns>The generated JWT token.</returns>
        public string 
        CreateToken
        (
            UserDto user
        )
        {
            var singinCredentials = GetSinginCredentials();
            var claims = GetClaims(user);
            var token = GenerateToken(singinCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Gets the signing credentials for JWT token generation.
        /// </summary>
        /// <returns>The signing credentials.</returns>
        private SigningCredentials 
        GetSinginCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));

            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        /// <summary>
        /// Gets the claims for the specified user.
        /// </summary>
        /// <param name="user">The user information.</param>
        /// <returns>The list of claims.</returns>
        private List<Claim> 
        GetClaims
        (
            UserDto user
        )
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _jwtOptions.Value.Issuer)
            };

            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }

        /// <summary>
        /// Generates a JWT token with the specified signing credentials and claims.
        /// </summary>
        /// <param name="singinCredentials">The signing credentials.</param>
        /// <param name="claims">The list of claims.</param>
        /// <returns>The generated JWT token.</returns>
        private JwtSecurityToken 
        GenerateToken
        (
            SigningCredentials singinCredentials, 
            List<Claim> claims
        )
        {
            var token = new JwtSecurityToken
            (
                issuer: _jwtOptions.Value.Issuer,
                claims: claims,
                expires: DateTime.Now.AddHours(Convert.ToDouble(_jwtOptions.Value.LifeTime)),
                signingCredentials: singinCredentials
            );

            return token;
        }
    }
}