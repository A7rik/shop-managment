using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Middleware
{
    public class RoleBasedAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleBasedAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userRole = context.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            var routeData = context.GetRouteData();
            var controller = routeData.Values["controller"]?.ToString();
            var action = routeData.Values["action"]?.ToString();
            var method = context.Request.Method;

            if (!IsRoleAuthorized(userRole, controller, action, method, context))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: Insufficient Role");
                return;
            }

            await _next(context);
        }

        private bool IsRoleAuthorized(string userRole, string controller, string action, string method, HttpContext context)
        {
            if (controller == "Products" || controller == "Categories")
            {
                if ((method == "POST" || method == "PUT" || method == "DELETE") && (userRole == "3" || userRole == "4"))
                {
                    return true;
                }
                if (method == "GET")
                {
                    return true;
                }
            }
            else if (controller == "Users")
            {
                if (userRole == "4")
                {
                    return true;
                }
                if ((method == "PUT" || method == "DELETE") && action == "UpdateUser")
                {
                    var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    var routeUserId = context.GetRouteData().Values["id"]?.ToString();
                    if (userId == routeUserId)
                    {
                        return true;
                    }
                }
            }
            else if (controller == "Auth")
            {
                if (userRole != null)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

    }
}
