using School.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace School.Attibutes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public bool AllowMultiple => throw new NotImplementedException();

        private string[] roles;

        public AuthorizeAttribute(params string[] roles)
        {
            this.roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var user = (Model.Entity.User)context.HttpContext.Items["User"];
            bool checkIfAllowed = true;
            if (user == null)
            {
                context.Result = new JsonResult(new CommonResponse(false, null, "Token expired")) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else {
                if (this.roles.Length > 0)
                {
                    checkIfAllowed = this.roles.ToList().Where(e => e.Equals(user.Role.Name, StringComparison.OrdinalIgnoreCase)).Any();
                }
                if (!checkIfAllowed) {
                    context.Result = new JsonResult(new CommonResponse(false, null, "Unauthorized")) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}
