using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? Array.Empty<Role>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
            {
                return;
            }


            // user returns "null" - should return a userlogin when authorized
            UserResponse user = (UserResponse)context.HttpContext.Items["Login"];
            if (user == null || (_roles.Any() && !_roles.Contains(user.Login.Type)))
            {
                //outcommented as workaround untill fixed
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}