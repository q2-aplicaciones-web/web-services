using System;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;

namespace Q2.WebService.API.ProductCatalog.Domain.Model.Commands
{
    /// <summary>
    /// Command to update the price of a product
    /// </summary>
    public record UpdateProductPriceCommand(
        Guid ProductId,
        Money NewPrice)
    {
        /// <summary>
        /// Alternative constructor accepting string ID and decimal price
        /// </summary>
        public UpdateProductPriceCommand(
            string productId,
            decimal amount,
            string currency)
            : this(
                Guid.Parse(productId),
                new Money(amount, currency))
        {
        }
    }
}
