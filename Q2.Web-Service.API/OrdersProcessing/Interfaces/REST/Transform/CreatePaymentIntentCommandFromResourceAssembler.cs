using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Commands;
using Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Resources;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform CreatePaymentIntentResource to CreatePaymentIntentCommand
/// </summary>
public static class CreatePaymentIntentCommandFromResourceAssembler
{
    /// <summary>
    /// Creates a CreatePaymentIntentCommand from a CreatePaymentIntentResource
    /// </summary>
    /// <param name="resource">The resource to transform</param>
    /// <returns>The command object</returns>
    /// <exception cref="ArgumentException">Thrown when resource is null or invalid</exception>
    public static CreatePaymentIntentCommand CreateCommandFromResource(CreatePaymentIntentResource resource)
    {
        if (resource == null)
            throw new ArgumentException("Resource cannot be null");

        // Validate the resource
        resource.Validate();

        // Create Money value object
        var amount = new Money(resource.Amount, resource.Currency);

        // Create and return the command
        return new CreatePaymentIntentCommand(amount);
    }
}
