namespace WebApi.Authorization
{
    public class JwtMiddleware
    {

        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILoginService loginService, IJwtUtils jwtUtils)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int? loginId = jwtUtils.ValidateJwtToken(token!);
            if (loginId != null)
            {
                context.Items["Login"] = await loginService.GetByIdAsync(loginId.Value);
            }

            await _next(context);
        }
    }
}
