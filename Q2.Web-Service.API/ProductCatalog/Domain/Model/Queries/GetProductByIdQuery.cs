using System;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Model.Queries
{
    public record GetProductByIdQuery
    {
        public Guid ProductId { get; }

        public GetProductByIdQuery(Guid productId)
        {
            ProductId = productId;
        }
    }
}
