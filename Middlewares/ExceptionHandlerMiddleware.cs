using Sales.Exceptions;

namespace Sales.Middlewares;

public class ExceptionHandlerMiddleware
{
    
    private readonly RequestDelegate _next;
    
    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = e switch
        {
            InvalidPriceException => StatusCodes.Status400BadRequest,
            InvalidTokenException => StatusCodes.Status401Unauthorized,
            NotFoundException => StatusCodes.Status404NotFound,
            OutOfStockException => StatusCodes.Status400BadRequest,
            RepositoryNotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var response = new
        {
            message = e.Message,
        };
        
        await context.Response.WriteAsJsonAsync(response);
    }


}