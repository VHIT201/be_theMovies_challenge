using Common.Application.Models;
using Common.Application.Settings;

namespace Application.Settings
{
    public class PermissionSetting : BasePermissionSetting
    {
        public override Dictionary<string, List<string>> ApiPermissions => new()
        {
            //{ "POST - /api/v1/identityusers/login", ["ADMIN", AuthenticatedUserModel.GuestRole]},
            { "POST - /api/v1/identityusers/register", ["ADMIN", AuthenticatedUserModel.GuestRole]},
        };


    }
}
