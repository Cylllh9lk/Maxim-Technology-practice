using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApplication7.Models
{
    public static class AppConfig
    {
        public static IConfiguration Configuration { get; private set; }

        static AppConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public static void Initialize()
        {
            // Метод для явной инициализации конфигурации
        }
    }

    public class RequestLimitMiddleware
    {
        private static int _requestCount = 3;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly RequestDelegate _next;
        private readonly int _maxRequests;

        public RequestLimitMiddleware(RequestDelegate next, int maxRequests)
        {
            _next = next;
            _maxRequests = maxRequests;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _semaphore.WaitAsync();

            try
            {
                if (_requestCount >= _maxRequests)
                {
                    context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    await context.Response.WriteAsync("HTTP ошибка 503 Service Unavailable");
                    return;
                }

                Interlocked.Increment(ref _requestCount);
            }
            finally
            {
                _semaphore.Release();
            }

            try
            {
                await _next(context);
            }
            finally
            {
                Interlocked.Decrement(ref _requestCount);
            }
        }
    }
}
