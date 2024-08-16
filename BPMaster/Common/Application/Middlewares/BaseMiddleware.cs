using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Common.Application.Middlewares
{
    public abstract class BaseMiddleware
    {
        protected readonly RequestDelegate next;
        public BaseMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public abstract Task Invoke(HttpContext context, IServiceProvider services, IConfiguration configuration);
    }
}
