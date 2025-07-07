using System.ComponentModel.DataAnnotations;

namespace Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Resources;

/// <summary>
/// Resource for creating a payment intent request
/// </summary>
public record CreatePaymentIntentResource
{
    /// <summary>
    /// The amount for the payment intent
    /// </summary>
    [Required(ErrorMessage = "Amount is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be a positive number")]
    public decimal Amount { get; init; }

    /// <summary>
    /// The currency code (ISO 4217)
    /// </summary>
    [Required(ErrorMessage = "Currency is required")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be a 3-letter ISO code")]
    public string Currency { get; init; } = string.Empty;

    /// <summary>
    /// Validates the resource and throws ArgumentException if invalid
    /// </summary>
    public void Validate()
    {
        if (Amount <= 0)
            throw new ArgumentException("Amount must be a positive number");
        if (string.IsNullOrWhiteSpace(Currency) || Currency.Length != 3)
            throw new ArgumentException("Currency cannot be null or empty and must be a 3-letter code");
    }
}
