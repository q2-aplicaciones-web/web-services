using System;
namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource representation of a Product.
    /// Note: We use primitive types at the API boundary layer instead of value objects
    /// as part of the anti-corruption layer pattern.
    /// </summary>
    public record ProductResource
    {
        public Guid Id { get; init; }
        public Guid ProjectId { get; init; }
        public decimal PriceAmount { get; init; }
        public string PriceCurrency { get; init; }
        public string Status { get; init; }
        public string ProjectTitle { get; init; }
        public string ProjectPreviewUrl { get; init; }
        public Guid ProjectUserId { get; init; }
        public long LikeCount { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }

        public ProductResource(Guid id, Guid projectId, decimal priceAmount, string priceCurrency, string status, string projectTitle, string projectPreviewUrl, Guid projectUserId, long likeCount, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            ProjectId = projectId;
            PriceAmount = priceAmount;
            PriceCurrency = priceCurrency;
            Status = status;
            ProjectTitle = projectTitle;
            ProjectPreviewUrl = projectPreviewUrl;
            ProjectUserId = projectUserId;
            LikeCount = likeCount;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
