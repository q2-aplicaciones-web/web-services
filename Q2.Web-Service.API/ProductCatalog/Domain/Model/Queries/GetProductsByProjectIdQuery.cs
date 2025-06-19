using Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects;

namespace Q2.WebService.API.ProductCatalog.Domain.Model.Queries
{
    /// <summary>
    /// Query to get products by project ID
    /// </summary>
    public record GetProductsByProjectIdQuery(ProjectId ProjectId)
    {
        /// <summary>
        /// Alternative constructor accepting string ID
        /// </summary>
        public GetProductsByProjectIdQuery(string projectId)
            : this(ProjectId.Of(projectId))
        {
        }
    }
}
