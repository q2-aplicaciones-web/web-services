using System;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Model.Commands
{
    public record DeleteProductCommand
    {
        public Guid ProductId { get; }

        public DeleteProductCommand(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("Product ID cannot be empty", nameof(productId));
            ProductId = productId;
        }
    }
}
