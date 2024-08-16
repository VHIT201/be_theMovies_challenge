using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Common.Constants
{
	public class ErrorCode
	{
		public static readonly string ValidationFailed = "ValidationFailed";
        public static readonly string AuthenticationFailed = "AuthenticationFailed";
        public static readonly string AuthorizationFailed = "AuthorizationFailed";
        public static readonly string NeedPasswordChange = "NeedPasswordChange";
    }
}
