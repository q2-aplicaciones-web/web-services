using System;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Model.Queries
{
    public record CheckIfUserLikedProductQuery
    {
        public Guid ProductId { get; }
        public Guid UserId { get; }

        public CheckIfUserLikedProductQuery(Guid productId, Guid userId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("Product ID cannot be empty", nameof(productId));
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty", nameof(userId));
            ProductId = productId;
            UserId = userId;
        }
    }
}
