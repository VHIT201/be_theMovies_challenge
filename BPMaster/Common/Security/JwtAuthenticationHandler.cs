using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Common.Application.Exceptions;
using Common.Application.Settings;
using Common.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;


namespace Common.Security
{
    public class JwtAuthenticationOptions : AuthenticationSchemeOptions
    {
    }

    public class JwtAuthenticationHandler<S>(IOptionsMonitor<JwtAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, AuthUserService<S> authService) 
        : AuthenticationHandler<JwtAuthenticationOptions>(options, logger, encoder) where S : BaseAppSetting
    {
        private readonly AuthUserService<S> _authUserService = authService;

       
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var endpoint = $"{ Request.Method } - { Request.Path.Value }";

            var permisionSetting = _authUserService.GetPermissionSetting();

            //Process for endpoint defined permission only
            if (permisionSetting.ApiPermissions.ContainsKey(endpoint))
            {
                _authUserService.ApiEndpoint = endpoint;
                _authUserService.Exception = ValidateRequest(out string jwt);                        
                _authUserService.JwtToken = jwt;
            }
       
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        private NonAuthenticateException? ValidateRequest(out string jwtToken)
        {
            jwtToken = string.Empty;
            //No Authorization Header
            if (!Request.Headers.TryGetValue("Authorization", out StringValues token))
            {
                return new NonAuthenticateException("Miss token value");
            }

            //No value in Authorization Header
            string authorizationHeader = token.ToString();
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return new NonAuthenticateException("Miss token value");
            }

            //Not in Bear format
            if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return new NonAuthenticateException("Invalid Bear token");
            }
            jwtToken = authorizationHeader["bearer".Length..].Trim();

            //Empty jwt
            if (string.IsNullOrEmpty(jwtToken))
            {
                return new NonAuthenticateException("Invalid JWT token");
            }

            return null;
        }
    }
}
