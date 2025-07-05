using Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Model.Queries
{
    public record GetProductsByProjectIdQuery
    {
        public ProjectId ProjectId { get; }

        public GetProductsByProjectIdQuery(ProjectId projectId)
        {
            ProjectId = projectId;
        }

        public GetProductsByProjectIdQuery(string projectId)
            : this(new ProjectId(projectId))
        {
        }
    }
}
