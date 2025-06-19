using System;

namespace Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects
{
    /// <summary>
    /// Represents a Project identifier as a value object
    /// </summary>
    public record ProjectId
    {
        public Guid Value { get; }

        private ProjectId(Guid value)
        {
            Value = value;
        }

        public static ProjectId Of(Guid value)
        {
            return new ProjectId(value);
        }

        public static ProjectId Of(string value)
        {
            return new ProjectId(Guid.Parse(value));
        }

        public static implicit operator Guid(ProjectId projectId) => projectId.Value;
        
        public override string ToString() => Value.ToString();
    }
}
