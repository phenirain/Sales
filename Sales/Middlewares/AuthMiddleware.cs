using System.Security.Claims;
using Sales.Support.Jwt;

namespace Sales.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthMiddleware> _logger;
    
    public AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authHeader) &&
                authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                var principal = JwtHandler.ValidateToken(token);
                if (principal != null)
                {
                    context.User = principal;
                }
            }

        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during authentication: {ex.Message}");
        }
        finally
        {
            await _next(context);
        }
    }
}