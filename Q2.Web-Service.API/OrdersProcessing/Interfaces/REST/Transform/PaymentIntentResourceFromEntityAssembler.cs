using Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Resources;
using Stripe;

namespace Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform Stripe PaymentIntent to PaymentIntentResource
/// </summary>
public static class PaymentIntentResourceFromEntityAssembler
{
    /// <summary>
    /// Transforms a Stripe PaymentIntent to a PaymentIntentResource
    /// </summary>
    /// <param name="paymentIntent">The Stripe PaymentIntent object</param>
    /// <returns>The resource with the client secret</returns>
    /// <exception cref="ArgumentNullException">Thrown when paymentIntent is null</exception>
    public static PaymentIntentResource ToResourceFromEntity(PaymentIntent paymentIntent)
    {
        if (paymentIntent == null)
            throw new ArgumentNullException(nameof(paymentIntent));

        return new PaymentIntentResource(paymentIntent.ClientSecret);
    }
}
