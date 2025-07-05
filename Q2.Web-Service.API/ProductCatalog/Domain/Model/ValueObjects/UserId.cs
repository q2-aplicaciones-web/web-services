using System;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects
{
    public record UserId(Guid Id)
    {
        public static UserId Of(Guid id) => new UserId(id);
        public static UserId Of(string id) => new UserId(Guid.Parse(id));
    }
}
