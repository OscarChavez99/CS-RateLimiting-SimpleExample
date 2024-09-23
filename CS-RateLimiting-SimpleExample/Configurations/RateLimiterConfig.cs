using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace CS_RateLimiting_SimpleExample.Configurations
{
    public static class RateLimiterConfig
    {
        // Registers all custom RateLimiters for the application
        public static void AddRateLimiterServices(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                AddTwoTimesPerIpLimiter(options);
                AddThreeTimesPerMinuteLimiter(options);
            });
        }

        private static void AddTwoTimesPerIpLimiter(RateLimiterOptions options)
        {
            options.AddPolicy("twoTimesPerIp", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 2,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));

            // 'Else'
            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.HttpContext.Response.WriteAsync("Rate limit exceeded for your IP. Please try again later.", cancellationToken);
            };
        }

        private static void AddThreeTimesPerMinuteLimiter(RateLimiterOptions options)
        {
            options.AddFixedWindowLimiter("threeTimesPerMinute", opt =>
            {
                opt.PermitLimit = 3;
                opt.Window = TimeSpan.FromMinutes(1);
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                opt.QueueLimit = 2;
            });
        }
    }
}
