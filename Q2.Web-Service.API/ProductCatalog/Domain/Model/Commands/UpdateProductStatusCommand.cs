using System;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Model.Commands
{
    public record UpdateProductStatusCommand
    {
        public Guid ProductId { get; }
        public ProductStatus Status { get; }

        public UpdateProductStatusCommand(Guid productId, ProductStatus status)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("Product ID cannot be empty", nameof(productId));
            // ProductStatus is an enum, so no null check needed unless nullable
            ProductId = productId;
            Status = status;
        }
    }
}
