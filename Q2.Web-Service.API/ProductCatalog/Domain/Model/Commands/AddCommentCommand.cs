using System;
using Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects;

namespace Q2.WebService.API.ProductCatalog.Domain.Model.Commands
{
    /// <summary>
    /// Command to add a comment to a product
    /// </summary>
    public record AddCommentCommand(
        Guid ProductId,
        UserId UserId,
        string Text)
    {
        /// <summary>
        /// Alternative constructor accepting string IDs
        /// </summary>
        public AddCommentCommand(
            string productId,
            string userId,
            string text)
            : this(
                Guid.Parse(productId),
                UserId.Of(userId),
                text)
        {
        }
    }
}
