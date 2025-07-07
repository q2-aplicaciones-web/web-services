using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Q2.Web_Service.API.Shared.Infrastructure.Stripe.Configuration;

/// <summary>
/// Configuration options for Stripe integration
/// </summary>
public class StripeSettings
{
    public const string SectionName = "Stripe";
    
    /// <summary>
    /// Stripe Secret API Key
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;
    
    /// <summary>
    /// Stripe Publishable Key (for frontend)
    /// </summary>
    public string PublishableKey { get; set; } = string.Empty;
    
    /// <summary>
    /// Webhook endpoint secret for signature verification
    /// </summary>
    public string WebhookSecret { get; set; } = string.Empty;
}

/// <summary>
/// Extension methods for configuring Stripe services
/// </summary>
public static class StripeConfigurationExtensions
{
    /// <summary>
    /// Adds Stripe services to the dependency injection container
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configuration">The configuration</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddStripeServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure Stripe settings
        services.Configure<StripeSettings>(configuration.GetSection(StripeSettings.SectionName));
        
        // Register Stripe service
        services.AddScoped<IStripeService, Services.StripeService>();
        
        // Configure Stripe API key globally
        services.AddSingleton<IStripeInitializer, StripeInitializer>();
        
        return services;
    }
}

/// <summary>
/// Interface for Stripe initialization
/// </summary>
public interface IStripeInitializer
{
    /// <summary>
    /// Initializes Stripe with the configured API key
    /// </summary>
    void Initialize();
}

/// <summary>
/// Initializes Stripe configuration on application startup
/// </summary>
public class StripeInitializer : IStripeInitializer
{
    private readonly StripeSettings _stripeSettings;
    private static bool _isInitialized = false;
    private static readonly object _lock = new object();

    public StripeInitializer(IOptions<StripeSettings> stripeSettings)
    {
        _stripeSettings = stripeSettings.Value;
    }

    /// <summary>
    /// Initializes Stripe with the configured API key
    /// </summary>
    public void Initialize()
    {
        if (_isInitialized) return;
        
        lock (_lock)
        {
            if (_isInitialized) return;
            
            if (string.IsNullOrWhiteSpace(_stripeSettings.SecretKey))
                throw new InvalidOperationException("Stripe:SecretKey configuration is missing or empty");
            
            // Set global Stripe API key
            global::Stripe.StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            
            _isInitialized = true;
        }
    }
}
