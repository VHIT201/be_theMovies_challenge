using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Models;
using Common.Application.Settings;
using Common.Loggers.Interfaces;
using Common.Utilities;
using Microsoft.AspNetCore.Http;

namespace Common.Services
{
    public class AuthUserService<S>(ILogManager logManager, S setting) where S : BaseAppSetting
    {
        private readonly BasePermissionSetting _permissionSetting = setting.PermissionSetting;
        private readonly JwtTokenSetting _jwtTokenSetting = setting.JwtTokenSetting;
        private ILogManager _logManager = logManager;
        //private readonly HttpContext _context = httpContextAccessor.HttpContext ?? throw new ApplicationException("Cannot get HttpContext of request");
        
        public Exception? Exception { get; set; }
        public string JwtToken { get; set; } = string.Empty;
        public string? ApiEndpoint { get; set; }

        private AuthenticatedUserModel? _user;
      
       
        public BasePermissionSetting GetPermissionSetting()
        {
            return _permissionSetting;
        }

        public AuthenticatedUserModel? GetUser()
        {
            if (string.IsNullOrEmpty(ApiEndpoint))
            {
                return null;
            }

            if (_user == null)
            {
                try
                {
                    if (Exception != null)
                    {
                        throw Exception;
                    }

                    //User is set one time only
                    _user = JwtUtil.VerifyAndGetUserModelFromJwtToken(JwtToken, _jwtTokenSetting);
                }
                catch (Exception ex)
                {
                    _logManager.Error(ex, $"Error with request {ApiEndpoint}");
                    throw;
                }
            }
            return _user;
        }
    }
}
