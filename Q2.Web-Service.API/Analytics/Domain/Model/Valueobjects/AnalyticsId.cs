// Q2.Web-Service.API/analytics/domain/model/valueobjects/AnalyticsId.cs
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Q2.Web_Service.API.Analytics.Domain.Model.ValueObjects
{
    [ComplexType]
    public class AnalyticsId : IEquatable<AnalyticsId>
    {
        [Column("id")]
        public string Value { get; private set; } = string.Empty;

        // Constructor protegido requerido por EF Core
        protected AnalyticsId() 
        { 
            Value = string.Empty; 
        }

        public AnalyticsId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("AnalyticsId cannot be null or blank", nameof(value));
            Value = value;
        }

        // Constructor interno para EF Core que permite valores temporalmente vacíos
        internal AnalyticsId(string value, bool allowEmpty)
        {
            Value = value;
        }

        // Método factory para crear desde un string, usado por EF Core
        public static AnalyticsId FromString(string value)
        {
            return new AnalyticsId(value);
        }

        public override bool Equals(object? obj) => Equals(obj as AnalyticsId);

        public bool Equals(AnalyticsId? other) =>
            other != null && Value == other.Value;

        public override int GetHashCode() => Value != null ? Value.GetHashCode() : 0;

        public override string ToString() => Value;
    }
}