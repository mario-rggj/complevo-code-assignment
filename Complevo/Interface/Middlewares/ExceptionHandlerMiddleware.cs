using System.Net;
using Newtonsoft.Json;

namespace Complevo.Interface.Middlewares;

public class ExceptionHandlerMiddleware
{
  private readonly RequestDelegate Next;

  public ExceptionHandlerMiddleware(RequestDelegate next)
  {
    Next = next;
  }

  public async Task Invoke(HttpContext context)
  {
    try
    {
      await Next(context);
    }
    catch (Exception exception)
    {
      await HandleExceptionAsync(context, exception);
    }
  }

  private static Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    Console.Error.Write(exception);
    var code = (int)HttpStatusCode.InternalServerError;
    var message = "Internal Server Error";
    const string contentType = "application/json";

    var content = JsonConvert.SerializeObject(new { error = message });
    context.Response.ContentType = contentType;
    context.Response.StatusCode = (int)code;
    return context.Response.WriteAsync(content);
  }
}