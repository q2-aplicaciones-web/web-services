using System;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Model.Commands
{
    public record CreateProductCommand
    {
        public ProjectId ProjectId { get; }
        public Money Price { get; }
        public ProductStatus Status { get; }
        public string ProjectTitle { get; }
        public string ProjectPreviewUrl { get; }
        public Guid ProjectUserId { get; }

        public CreateProductCommand(ProjectId projectId, Money price, ProductStatus status, string projectTitle, string projectPreviewUrl, Guid projectUserId)
        {
            if (projectId == null)
                throw new ArgumentException("Project ID cannot be null");
            if (price == null)
                throw new ArgumentException("Price cannot be null");
            if (string.IsNullOrWhiteSpace(projectTitle))
                throw new ArgumentException("Project title cannot be null or empty");
            if (projectUserId == Guid.Empty)
                throw new ArgumentException("Project user ID cannot be null");

            ProjectId = projectId;
            Price = price;
            Status = status;
            ProjectTitle = projectTitle;
            ProjectPreviewUrl = projectPreviewUrl;
            ProjectUserId = projectUserId;
        }

        public CreateProductCommand(string projectId, Money price, ProductStatus status, string projectTitle, string projectPreviewUrl, Guid projectUserId)
            : this(new ProjectId(projectId), price, status, projectTitle, projectPreviewUrl, projectUserId)
        {
        }
    }
}
