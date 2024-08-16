using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Common.Application.Settings
{
    public abstract class BasePermissionSetting
    {
        //[Endpoint : List of Permission]
        public abstract Dictionary<string, List<string>> ApiPermissions {  get; }

    }
}
