using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Enums;

namespace Domain.Entities
{
    [Table("identity_users")]
    public class IdentityUser : SystemLogEntity<Guid>
    {  
        public string Username { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Password { get; set; } = string.Empty;
        public AuthenticationType AuthenticationType { get; set; } = AuthenticationType.Default;
        public string Salts { get; set; } = string.Empty;
        public UserStatus Status { get; set; } = UserStatus.Active;
        public string Role { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
    }
}