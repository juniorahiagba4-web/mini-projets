using System.Diagnostics;
using System.Text.Json;

namespace BiblioApi.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            await _next(context);
            sw.Stop();
            var log = new
            {
                Path = context.Request.Path,
                Method = context.Request.Method,
                StatusCode = context.Response.StatusCode,
                DurationMs = sw.ElapsedMilliseconds
            };
            Console.WriteLine(JsonSerializer.Serialize(log));
        }
    }
}
