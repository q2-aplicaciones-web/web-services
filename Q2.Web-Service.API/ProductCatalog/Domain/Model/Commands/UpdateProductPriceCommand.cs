using System;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Model.Commands
{
    public record UpdateProductPriceCommand
    {
        public Guid ProductId { get; }
        public Money Price { get; }

        public UpdateProductPriceCommand(Guid productId, Money price)
        {
            ProductId = productId;
            Price = price;
        }
    }
}
