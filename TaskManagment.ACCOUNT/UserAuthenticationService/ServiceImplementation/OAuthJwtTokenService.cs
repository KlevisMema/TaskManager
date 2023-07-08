using System.Text;
using System.Security.Claims;
using TaskManager.DAL.DTO_s.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace TaskManagment.SECURITY.UserAuthenticationService.ServiceImplementation
{
    public class OAuthJwtTokenService
    {
        private readonly IOptions<AuthenticationSettings> _jwtOptions;

        public OAuthJwtTokenService
        (
            IOptions<AuthenticationSettings> jwtOptions
        )
        {
            _jwtOptions = jwtOptions;
        }

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

        private SigningCredentials
        GetSinginCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));

            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim>
        GetClaims
        (
           UserDto user
        )
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _jwtOptions.Value.Issuer)
            };

            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }

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