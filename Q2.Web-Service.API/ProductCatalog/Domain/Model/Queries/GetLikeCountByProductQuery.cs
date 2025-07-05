using System;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Model.Queries
{
    public record GetLikeCountByProductQuery
    {
        public Guid ProductId { get; }

        public GetLikeCountByProductQuery(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("Product ID cannot be empty", nameof(productId));
            ProductId = productId;
        }
    }
}
