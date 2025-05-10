using EmisTracking.Services.Entities;
using EmisTracking.Services.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EmisTracking.Services.JwtAuth
{
    public class JwtTokenService
    {
        private const int DaysExpirationTerm = 50;
        private readonly JwtTokenSettings _settings;

        public JwtTokenService(IOptions<JwtTokenSettings> options)
        {
            if (options != null)
            {
                _settings = options.Value;
            }
        }

        public string GetToken(SystemUser user, IList<string> roles, string roleInfo)
        {
            ArgumentNullException.ThrowIfNull(user);

            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
            var userClaims = new List<Claim>
            {
                new(type: Constants.UserIdClaimType, user.Id),
                new(type: JwtRegisteredClaimNames.GivenName, user.FullName),
                new(type: Constants.RoleInfoClaimType, roleInfo),
            };

            userClaims.AddRange(roleClaims);

            var signingCreds = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSecretKey)),
                    SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _settings.JwtIssuer,
                audience: _settings.JwtAudience,
                signingCredentials: signingCreds,
                claims: userClaims,
                expires: DateTime.Now.AddDays(DaysExpirationTerm));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _settings.JwtIssuer,
                    ValidateAudience = true,
                    ValidAudience = _settings.JwtAudience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSecretKey)),
                    ValidateLifetime = true,
                    LifetimeValidator =
                        (before, exprise, localToken, parameters) =>
                        {
                            if (exprise.HasValue)
                            {
                                return exprise.Value > DateTime.UtcNow;
                            }

                            return true;
                        },
                    RequireExpirationTime = true,
                },
                out var _);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }
    }
}
