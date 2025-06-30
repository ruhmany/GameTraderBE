using GameTrader.Core.DTOs.UserDTOs;
using GameTrader.Core.Interfaces.IServices;
using GameTrader.Core.StaticData;
using System.Net;

namespace GameTrader.API.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public AuthorizationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            (_next, _configuration) = (next, configuration);
        }

        public async Task Invoke(HttpContext httpContext, IUserService userService)
        {
            if (!AllowedPermissions.NoAuthenticationRoutes().Any(x => x.ToLower() == httpContext.Request.Path.Value.Replace("/api", "").ToLower()))
            {
                await ValidateTokenAsync(httpContext, userService);
            }
            else
            {
                await _next(httpContext);
            }
        }

        private async Task ValidateTokenAsync(HttpContext httpContext, IUserService userService)
        {
            using (HttpClient client = new())
            {
                var authorization = httpContext.Request.Headers["Authorization"].FirstOrDefault();

                if (authorization is null)
                    throw new NullReferenceException("Authorization Header is Null or Empty!");

                string token = httpContext.Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "").Trim();
                string url = string.Join("/", httpContext.Request.Path.Value.Replace("/api/", "").Split('/').Take(2));

                TokenValidationDTO validationDTO = new(token, url, httpContext.Request.Method);

                AuthorizationDTO responseDto;

                if (AllowedPermissions.RequireAuthenticationRoutes().Any(x => x.ToLower() == httpContext.Request.Path.Value.Replace("/api", "").ToLower()))
                {
                    responseDto = await userService.Authenticate(validationDTO);
                }
                else
                {
                    responseDto = await userService.Authorize(validationDTO);
                }

                if (responseDto.StatusCode.Equals(HttpStatusCode.OK) && !string.IsNullOrEmpty(responseDto.UserId))
                {
                    await _next(httpContext);
                }
                else if (responseDto.StatusCode.Equals(HttpStatusCode.Unauthorized))
                {
                    httpContext.Response.Headers.Add("Content-Type", "application/json");
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await httpContext.Response.CompleteAsync();
                }
                else if (responseDto.StatusCode.Equals(HttpStatusCode.Forbidden))
                {
                    httpContext.Response.Clear();
                    httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await httpContext.Response.CompleteAsync();
                }
            }
        }


    }
}
