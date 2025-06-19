using System;
using System.Collections.Generic;

namespace Q2.WebService.API.ProductCatalog.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource representing a product for API responses
    /// </summary>
    public class ProductResource
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ManufacturerId { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int Likes { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Gallery { get; set; }
        public double Rating { get; set; }
        public string Status { get; set; }
        public List<CommentResource> Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
