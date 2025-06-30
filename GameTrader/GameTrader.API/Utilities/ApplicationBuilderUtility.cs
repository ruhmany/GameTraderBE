using GameTrader.API.Middlewares;

namespace GameTrader.API.Utilities
{
    public static class ApplicationBuilderUtility
    {
        public static IApplicationBuilder UseTokenValidation(this IApplicationBuilder applicationBuilder)
              => applicationBuilder.UseMiddleware<AuthorizationMiddleware>();
    }
}
