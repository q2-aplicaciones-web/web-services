using System;
using System.Collections.Generic;
using System.Linq;
using Q2.Web_Service.API.Shared.Domain.Model.Common;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;
using Q2.WebService.API.ProductCatalog.Domain.Model.Commands;
using Q2.WebService.API.ProductCatalog.Domain.Model.Entities;
using Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects;

namespace Q2.WebService.API.ProductCatalog.Domain.Model.Aggregates
{
    /// <summary>
    /// Product aggregate root representing a product in the catalog
    /// </summary>
    public class Product : IAuditableEntity
    {
        private readonly List<Comment> _comments = new();
        private readonly List<string> _tags = new();
        private readonly List<string> _gallery = new();

        // Properties
        public Guid Id { get; private set; }
        public ProjectId ProjectId { get; private set; }
        public ManufacturerId ManufacturerId { get; private set; }
        public Money Price { get; private set; }
        public int Likes { get; private set; }
        public IReadOnlyCollection<string> Tags => _tags.AsReadOnly();
        public IReadOnlyCollection<string> Gallery => _gallery.AsReadOnly();
        public Rating Rating { get; private set; }
        public string Status { get; private set; }
        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // For EF Core
        protected Product() { }

        public Product(CreateProductCommand command)
        {
            Id = Guid.NewGuid();
            ProjectId = command.ProjectId;
            ManufacturerId = command.ManufacturerId;
            Price = command.Price;
            Likes = 0;
            
            if (command.Tags != null)
                _tags.AddRange(command.Tags);
                
            if (command.Gallery != null)
                _gallery.AddRange(command.Gallery);
                
            Rating = Rating.Of(0.0);
            Status = command.Status;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdatePrice(Money newPrice)
        {
            Price = newPrice;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveComment(Comment comment)
        {
            _comments.Remove(comment);
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveComment(Guid commentId)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == commentId);
            if (comment != null)
            {
                _comments.Remove(comment);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void IncrementLikes()
        {
            Likes++;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DecrementLikes()
        {
            if (Likes > 0)
            {
                Likes--;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void UpdateRating(double newRating)
        {
            Rating = Rating.Of(newRating);
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddTag(string tag)
        {
            if (!string.IsNullOrWhiteSpace(tag) && !_tags.Contains(tag))
            {
                _tags.Add(tag);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void RemoveTag(string tag)
        {
            if (!string.IsNullOrWhiteSpace(tag) && _tags.Contains(tag))
            {
                _tags.Remove(tag);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void AddImageToGallery(string imageUrl)
        {
            if (!string.IsNullOrWhiteSpace(imageUrl) && !_gallery.Contains(imageUrl))
            {
                _gallery.Add(imageUrl);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void RemoveImageFromGallery(string imageUrl)
        {
            if (!string.IsNullOrWhiteSpace(imageUrl) && _gallery.Contains(imageUrl))
            {
                _gallery.Remove(imageUrl);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void UpdateStatus(string newStatus)
        {
            Status = !string.IsNullOrWhiteSpace(newStatus) 
                ? newStatus 
                : throw new ArgumentException("Status cannot be empty", nameof(newStatus));
            
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
