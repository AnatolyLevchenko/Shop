using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
          logger.LogError(ex,ex.Message);
          context.Response.ContentType = "application/json";
          context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

          var response = env.IsDevelopment()
              ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
              : new ApiException((int)HttpStatusCode.InternalServerError);



          var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase
          });
          await context.Response.WriteAsync(json);
        }
    }

}