using System;

namespace Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects
{
    /// <summary>
    /// Value object representing a reference to a User within the Product Catalog domain.
    /// </summary>
    /// <remarks>
    /// This is an intentionally simplified identifier that contains only what the Product Catalog
    /// domain needs to know about a User, without any implementation details of external systems.
    /// The Application layer's ACL is responsible for translating between this simple identifier
    /// and any external system representations.
    /// </remarks>
    public record UserId
    {
        /// <summary>
        /// The string value of the user ID
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates a new UserId with validation
        /// </summary>
        private UserId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("User ID cannot be null or empty", nameof(value));
                
            Value = value;
        }

        /// <summary>
        /// Factory method to create a UserId from a string
        /// </summary>
        /// <param name="id">The string representation of the user ID</param>
        /// <returns>A new UserId instance</returns>
        public static UserId Of(string id)
        {
            return new UserId(id);
        }

        /// <summary>
        /// Factory method for creating a UserId from a Guid value
        /// </summary>
        /// <param name="id">The Guid user ID</param>
        /// <returns>A new UserId instance</returns>
        public static UserId Of(Guid id)
        {
            return new UserId(id.ToString());
        }
        
        public override string ToString() => Value;
    }
}
