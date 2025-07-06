using System;
using Q2.Web_Service.API.DesignLab.Interfaces.ACL;

namespace Q2.Web_Service.API.ProductCatalog.Interfaces.ACL
{
    /// <summary>
    /// Implementación concreta que delega al ProjectContextFacadeImpl principal
    /// </summary>
    public class ProjectContextFacade : IProjectContextFacade
    {
        private readonly Q2.Web_Service.API.DesignLab.Interfaces.ACL.IProjectContextFacade _projectContextFacade;

        public ProjectContextFacade(Q2.Web_Service.API.DesignLab.Interfaces.ACL.IProjectContextFacade projectContextFacade)
        {
            _projectContextFacade = projectContextFacade;
        }

        public bool ProjectExists(Guid projectId)
        {
            return _projectContextFacade.ProjectExists(projectId);
        }

        public ProjectDetails? FetchProjectDetailsForProduct(Guid projectId)
        {
            return _projectContextFacade.FetchProjectDetailsForProduct(projectId);
        }

        public long GetProjectCountByUserId(Guid userId)
        {
            return _projectContextFacade.GetProjectCountByUserId(userId);
        }

        public System.Collections.Generic.List<Guid> FetchProjectIdsByUserId(Guid userId)
        {
            return _projectContextFacade.FetchProjectIdsByUserId(userId);
        }
    }
}
