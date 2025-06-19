using System;

namespace Q2.WebService.API.ProductCatalog.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource representing a comment for API responses
    /// </summary>
    public class CommentResource
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
