using System;
namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources
{
    public record ProductLikeStatusResource
    {
        public string Message { get; }
        public Guid ProductId { get; }
        public Guid UserId { get; }

        public ProductLikeStatusResource(string message, Guid productId, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message cannot be null or blank", nameof(message));
            if (productId == Guid.Empty)
                throw new ArgumentException("Product ID cannot be empty", nameof(productId));
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty", nameof(userId));
            Message = message;
            ProductId = productId;
            UserId = userId;
        }
    }
}
