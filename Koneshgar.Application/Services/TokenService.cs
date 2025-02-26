using Koneshgar.Application.Services.Abstract;
using Koneshgar.Application.Configurations;
using Koneshgar.Application.Utilities.Helpers;
using Koneshgar.Domain.Dtos.User;
using Koneshgar.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Koneshgar.Application.Services.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly JWTOptions _jwtOptions;
        private readonly UserManager<User> _userManager;
        public TokenService(IOptions<JWTOptions> options, UserManager<User> userManager)
        {
            _jwtOptions = options.Value;
            _userManager = userManager;
        }

        public async Task<TokenDTO> CreateToken(User user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_jwtOptions.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_jwtOptions.RefreshTokenExpiration);
            var securityKey = SecurityKeyHelper.GetSymmetricSecurityKey(_jwtOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);


            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience[0],
                expires: accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: await GetClaims(user, _jwtOptions.Audience),
                 signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDTO
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
            };

            return tokenDto;
        }


        private async Task<IEnumerable<Claim>> GetClaims(User user, List<string> audiences)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return claims;
        }
    }
}
