using System;
using Q2.Web_Service.API.Shared.Domain.Model.Common;
using Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects;

namespace Q2.WebService.API.ProductCatalog.Domain.Model.Entities
{
    /// <summary>
    /// Represents a comment on a product
    /// </summary>
    public class Comment : IAuditableEntity
    {
        public Guid Id { get; private set; }
        public UserId UserId { get; private set; }
        public string Text { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // For EF Core
        protected Comment() { }

        public Comment(UserId userId, string text)
        {
            Id = Guid.NewGuid();
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            Text = !string.IsNullOrWhiteSpace(text) 
                ? text 
                : throw new ArgumentException("Comment text cannot be empty", nameof(text));
            
            CreatedAt = DateTime.UtcNow;
        }

        public Comment(string userId, string text)
            : this(UserId.Of(userId), text)
        {
        }

        public void UpdateText(string newText)
        {
            if (string.IsNullOrWhiteSpace(newText))
                throw new ArgumentException("Comment text cannot be empty", nameof(newText));
            
            Text = newText;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
