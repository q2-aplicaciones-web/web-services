namespace Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Resources;

/// <summary>
/// Resource containing the payment intent response with client secret
/// </summary>
/// <param name="SecretKey">The Stripe client secret for completing the payment on the frontend</param>
public record PaymentIntentResource(string SecretKey);
