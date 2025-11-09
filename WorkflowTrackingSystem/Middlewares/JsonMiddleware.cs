using Newtonsoft.Json;
using System.Text;

namespace Workflow.Presentation.Middleware;

public class JsonMiddleware
{
    private readonly RequestDelegate _next;

    public JsonMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.ContentType != null && context.Request.ContentType.Contains("application/json"))
        {
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            if (!string.IsNullOrWhiteSpace(body))
            {
                try
                {
                    var parsed = JsonConvert.DeserializeObject<object>(body);
                    context.Items["ParsedJson"] = parsed;
                }
                catch (JsonException ex)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync($"Invalid JSON: {ex.Message}");
                    return;
                }
            }
        }

        await _next(context);
    }
}

public static class JsonMiddlewareExtensions
{
    public static IApplicationBuilder UseJsonMiddleware(this IApplicationBuilder app) => app.UseMiddleware<JsonMiddleware>();
}
