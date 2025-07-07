using System;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects
{
    // Value object for ProjectId
    public sealed record ProjectId
    {
        public string Value { get; }

        public ProjectId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Project ID cannot be null or blank", nameof(value));
            Value = value;
        }

        public static ProjectId Of(string id) => new ProjectId(id);
        public static ProjectId Of(Guid id) => new ProjectId(id.ToString());

        public override string ToString() => Value;
    }
}
