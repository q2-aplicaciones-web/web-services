using Q2.Web_Service.API.Shared.Infrastructure.Stripe.Configuration;

namespace Q2.Web_Service.API.Shared.Infrastructure.Stripe.Middleware;

/// <summary>
/// Middleware to initialize Stripe configuration on application startup
/// </summary>
public class StripeInitializationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IStripeInitializer _stripeInitializer;
    private static bool _isInitialized = false;
    private static readonly object _lock = new object();

    public StripeInitializationMiddleware(RequestDelegate next, IStripeInitializer stripeInitializer)
    {
        _next = next;
        _stripeInitializer = stripeInitializer;
    }

    /// <summary>
    /// Invokes the middleware and ensures Stripe is initialized
    /// </summary>
    /// <param name="context">The HTTP context</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        if (!_isInitialized)
        {
            lock (_lock)
            {
                if (!_isInitialized)
                {
                    _stripeInitializer.Initialize();
                    _isInitialized = true;
                }
            }
        }

        await _next(context);
    }
}

/// <summary>
/// Extension method to add Stripe initialization middleware
/// </summary>
public static class StripeInitializationMiddlewareExtensions
{
    /// <summary>
    /// Adds Stripe initialization middleware to the pipeline
    /// </summary>
    /// <param name="builder">The application builder</param>
    /// <returns>The application builder for chaining</returns>
    public static IApplicationBuilder UseStripeInitialization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<StripeInitializationMiddleware>();
    }
}
