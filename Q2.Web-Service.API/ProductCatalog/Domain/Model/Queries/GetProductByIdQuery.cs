using System;

namespace Q2.WebService.API.ProductCatalog.Domain.Model.Queries
{
    /// <summary>
    /// Query to get a product by its ID
    /// </summary>
    public record GetProductByIdQuery(Guid ProductId)
    {
        /// <summary>
        /// Alternative constructor accepting string ID
        /// </summary>
        public GetProductByIdQuery(string productId)
            : this(Guid.Parse(productId))
        {
        }
    }
}
