using System;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates
{
    public class ProductLike
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

        public ProductLike() { }

        public ProductLike(Guid productId, Guid userId)
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
