using System;

namespace Q2.Web_Service.API.ProductCatalog.Interfaces.ACL
{
    public class ProjectContextFacade : IProjectContextFacade
    {
        public bool ProjectExists(Guid projectId)
        {
            // TODO: Implement actual logic
            return true;
        }

        public dynamic FetchProjectDetailsForProduct(Guid projectId)
        {
            // TODO: Implement actual logic
            return new { Id = projectId, Title = "Stub Project", PreviewUrl = "stub-url", UserId = Guid.NewGuid() };
        }
    }
}
