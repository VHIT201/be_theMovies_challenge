using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Application.Exceptions;
using Common.Application.Models;
using Common.Application.Settings;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;

namespace Common.Utilities
{
    public static class JwtUtil
    {
        public static string CreateJwtToken(JwtTokenSetting jwtTokenSetting, AuthenticatedUserModel user )
        {
            //Time token expires
            var expires = DateTime.UtcNow.AddMinutes(jwtTokenSetting.ExpirationMinutes);
            DateTimeOffset dateTimeOffset = expires;

            //Signing key
            var signingCredentials = GetSignatureKey(jwtTokenSetting.SymmetricSecurityKey);
           
            //Claims in token
            var tokenClaims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Sub, jwtTokenSetting.JwtRegisteredClaimNamesSub),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new(JwtRegisteredClaimNames.Exp, dateTimeOffset.ToUnixTimeSeconds().ToString()),
                //NameIdentifier is auto generate
                new(ClaimTypes.Name, user.UserName),
            };

            tokenClaims.AddRange(UserClaims.GetTokenClaims(user));



            JwtSecurityToken token = new(
               jwtTokenSetting.Issuer,
               jwtTokenSetting.Audience,               
               tokenClaims,
               expires,
               signingCredentials: signingCredentials
            );
            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);

            //Decode
            VerifyAndGetUserModelFromJwtToken(encodeToken, jwtTokenSetting);

            return encodeToken;
        }

        public static AuthenticatedUserModel VerifyAndGetUserModelFromJwtToken(string jwtToken, JwtTokenSetting jwtTokenSetting)
        {
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSetting.SymmetricSecurityKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenSetting.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtTokenSetting.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);
   

                var result = UserClaims.GetAuthenticatedUser(principal.Claims);

                return result;

            } catch (Exception ex)
            {
                throw new NonAuthenticateException(ex.Message);
            }
        }

        private static SigningCredentials GetSignatureKey(string serectKey)
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(serectKey)
                ),
                SecurityAlgorithms.HmacSha256
            );
        }

        public static class UserClaims
        {
            public const string UserId = "user.id";
            public const string UserName = "user.username";
            public const string FirstName = "user.firstname";
            public const string LastName = "user.lastname";
            public const string Email = "user.email";

            public static List<Claim> GetTokenClaims(AuthenticatedUserModel user)
            {
                return
                [
                    new(UserId,user.UserId.ToString()),
                    new(UserName,user.UserName),
                    new(FirstName,user.FirstName ?? string.Empty),
                    new(LastName,user.LastName ?? string.Empty),
                    new(Email,user.Email ?? string.Empty),

                ];
            }

            public static AuthenticatedUserModel GetAuthenticatedUser(IEnumerable<Claim> claims)
            {
                Dictionary<string, Claim> claimDictionary = claims.ToDictionary(y => y.Type, y => y);

                return new()
                {
                    UserId = new Guid(claimDictionary[UserId].Value),
                    UserName = claimDictionary[UserName].Value,
                    FirstName = claimDictionary[FirstName].Value,
                    LastName = claimDictionary[LastName].Value,
                    Email = claimDictionary[Email].Value,
                };
            }
        }
    }

}
