using EmisTracking.Services.WebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace EmisTracking.WebApp.JwtAuth
{
    internal class JwtAuthenticationHandler(
        IAuthApiService authApiService,
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        private readonly IAuthApiService _authApiService = authApiService;

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Cookies.ContainsKey(Services.WebApi.Constants.JwtCookiesKey))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var token = Request.Cookies[Services.WebApi.Constants.JwtCookiesKey]!.ToString();

            var response = await _authApiService.GetAuthValidateToken(token);

            if (response.Success)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                var identity = new ClaimsIdentity(jwtToken.Claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Unauthorized");
        }
    }
}
