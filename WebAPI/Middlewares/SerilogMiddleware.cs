using Serilog.Context;
using System.Text;

namespace WebAPI.Middlewares;

public class SerilogMiddleware
{
    private readonly RequestDelegate _next;

    public SerilogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        httpContext.Request.EnableBuffering();

        //request_type
        var requestType = httpContext.Request.Method;
        LogContext.PushProperty("request_type", requestType);

        //user_name
        var username = httpContext.User?.Identity?.Name != null || true ? httpContext.User?.Identity?.Name : null;
        username = "Ömer";
        LogContext.PushProperty("user_name", username);

        //request_body
        var buffer = new byte[Convert.ToInt32(httpContext.Request.ContentLength)];
        await httpContext.Request.Body.ReadAsync(buffer);
        var requestBody = Encoding.UTF8.GetString(buffer).ToString();
        LogContext.PushProperty("request_body", requestBody);

        await _next(httpContext);
    }
}

public static class SerilogExtensions
{
    public static IApplicationBuilder UseSerilog(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SerilogMiddleware>();
    }
}
