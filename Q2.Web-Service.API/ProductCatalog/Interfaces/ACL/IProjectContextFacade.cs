using System;

namespace Q2.Web_Service.API.ProductCatalog.Interfaces.ACL
{
    // Minimal stub for IProjectContextFacade to resolve build errors
    public interface IProjectContextFacade
    {
        bool ProjectExists(Guid projectId);
        dynamic FetchProjectDetailsForProduct(Guid projectId); // Replace 'dynamic' with actual type if known
    }
}
