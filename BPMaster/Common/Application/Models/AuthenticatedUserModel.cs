using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Models
{
    public class AuthenticatedUserModel
    {
        public const string GuestRole = "GUEST";
        public Guid UserId { get; set; }
        public string UserName { get; set;} = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}
