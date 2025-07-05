using System;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Commands;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates
{
    public class Product
    {
        public Guid Id { get; set; }
        public ProjectId? ProjectId { get; set; }
        public Money Price { get; set; }
        public ProductStatus Status { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectPreviewUrl { get; set; }
        public Guid ProjectUserId { get; set; }
        public long LikeCount { get; set; } = 0L;

        // Default constructor for EF and serialization
        public Product()
        {
            // Do not set ProjectId here; EF Core will set it after construction.
            Price = new Money(0, Money.DEFAULT_CURRENCY);
            Status = ProductStatus.AVAILABLE;
            ProjectTitle = string.Empty;
            ProjectPreviewUrl = string.Empty;
            ProjectUserId = Guid.Empty;
        }

        public Product(CreateProductCommand command)
        {
            ProjectId = command.ProjectId;
            Price = command.Price;
            Status = command.Status;
            ProjectTitle = command.ProjectTitle;
            ProjectPreviewUrl = command.ProjectPreviewUrl;
            ProjectUserId = command.ProjectUserId;
        }

        public void UpdatePrice(Money newPrice)
        {
            if (newPrice == null)
                throw new ArgumentException("Price cannot be null");
            Price = newPrice;
        }

        public void UpdateStatus(ProductStatus newStatus)
        {
            Status = newStatus;
        }

        public void UpdateProjectInfo(string title, string previewUrl, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Project title cannot be null or empty");
            if (userId == Guid.Empty)
                throw new ArgumentException("Project user ID cannot be null");
            ProjectTitle = title;
            ProjectPreviewUrl = previewUrl;
            ProjectUserId = userId;
        }

        public void UpdateLikeCount(long count)
        {
            if (count < 0)
                throw new ArgumentException("Like count cannot be negative");
            LikeCount = count;
        }

        public void IncrementLikeCount()
        {
            LikeCount++;
        }

        public void DecrementLikeCount()
        {
            LikeCount = Math.Max(0, LikeCount - 1);
        }
    }
}
