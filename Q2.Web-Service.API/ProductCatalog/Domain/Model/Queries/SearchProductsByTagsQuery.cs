using System.Collections.Generic;

namespace Q2.WebService.API.ProductCatalog.Domain.Model.Queries
{
    /// <summary>
    /// Query to search products by tags
    /// </summary>
    public record SearchProductsByTagsQuery(List<string> Tags);
}
