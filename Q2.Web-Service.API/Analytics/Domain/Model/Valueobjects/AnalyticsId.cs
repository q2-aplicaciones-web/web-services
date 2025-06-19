// Q2.Web-Service.API/analytics/domain/model/valueobjects/AnalyticsId.cs
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quri.Teelab.Api.Teelab.Analytics.Domain.Model.ValueObjects
{
    [ComplexType]
    public class AnalyticsId : IEquatable<AnalyticsId>
    {
        [Column("id")]
        public string Value { get; private set; }

        // Constructor protegido requerido por EF Core
        protected AnalyticsId() { }

        public AnalyticsId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("AnalyticsId cannot be null or blank", nameof(value));
            Value = value;
        }

        public override bool Equals(object obj) => Equals(obj as AnalyticsId);

        public bool Equals(AnalyticsId other) =>
            other != null && Value == other.Value;

        public override int GetHashCode() => Value != null ? Value.GetHashCode() : 0;

        public override string ToString() => Value;
    }
}