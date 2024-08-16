using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Settings
{
    public class JwtTokenSetting
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SymmetricSecurityKey { get; set; } = string.Empty;
        public string JwtRegisteredClaimNamesSub { get; set; } = string.Empty;

        public int ExpirationMinutes { get; set; } = 10;
    }
}
