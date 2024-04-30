using System.Net;
using System.Text.Json;

namespace API;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        try{
            await _next(context);
        }
        catch(Exception ex){
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType  = "application/json"; // controllers automatically set this default
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500

            var response = _env.IsDevelopment() ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
            :new ApiException(context.Response.StatusCode, ex.Message, "internall server error"); // 

            var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase}; // controllers set by default


            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }



}
