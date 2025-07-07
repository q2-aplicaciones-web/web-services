using Stripe;

namespace Q2.Web_Service.API.Shared.Infrastructure.Stripe.Exceptions;

/// <summary>
/// Base exception for payment processing errors
/// </summary>
public abstract class PaymentException : Exception
{
    protected PaymentException(string message) : base(message) { }
    protected PaymentException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Exception thrown when a payment is declined
/// </summary>
public class PaymentDeclinedException : PaymentException
{
    public PaymentDeclinedException(string message) : base(message) { }
    public PaymentDeclinedException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Exception thrown when payment service is temporarily unavailable
/// </summary>
public class PaymentServiceUnavailableException : PaymentException
{
    public PaymentServiceUnavailableException(string message) : base(message) { }
    public PaymentServiceUnavailableException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Exception thrown when payment request is invalid
/// </summary>
public class InvalidPaymentRequestException : PaymentException
{
    public InvalidPaymentRequestException(string message) : base(message) { }
    public InvalidPaymentRequestException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Exception thrown for general payment processing errors
/// </summary>
public class PaymentProcessingException : PaymentException
{
    public PaymentProcessingException(string message) : base(message) { }
    public PaymentProcessingException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Helper class for converting Stripe exceptions to domain exceptions
/// </summary>
public static class StripeExceptionMapper
{
    /// <summary>
    /// Maps Stripe exceptions to domain-specific exceptions
    /// </summary>
    /// <param name="ex">The Stripe exception</param>
    /// <returns>A domain-specific exception</returns>
    public static PaymentException MapToDomainException(StripeException ex)
    {
        return ex switch
        {
            StripeException when ex.StripeError?.Type == "card_error" => 
                new PaymentDeclinedException($"Card was declined: {ex.Message}", ex),
            StripeException when ex.StripeError?.Type == "rate_limit_error" => 
                new PaymentServiceUnavailableException($"Too many requests: {ex.Message}", ex),
            StripeException when ex.StripeError?.Type == "invalid_request_error" => 
                new InvalidPaymentRequestException($"Invalid payment data: {ex.Message}", ex),
            StripeException when ex.StripeError?.Type == "authentication_error" => 
                new PaymentProcessingException($"Authentication failed: {ex.Message}", ex),
            StripeException when ex.StripeError?.Type == "api_connection_error" => 
                new PaymentServiceUnavailableException($"Connection failed: {ex.Message}", ex),
            StripeException when ex.StripeError?.Type == "api_error" => 
                new PaymentProcessingException($"API error: {ex.Message}", ex),
            _ => new PaymentProcessingException($"Payment failed: {ex.Message}", ex)
        };
    }
}
