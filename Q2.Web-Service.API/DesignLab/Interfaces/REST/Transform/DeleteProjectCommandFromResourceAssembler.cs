using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public static class DeleteProjectCommandFromResourceAssembler
{
    public static DeleteProjectCommand ToCommandFromResource(string projectId)
    {
        if (string.IsNullOrWhiteSpace(projectId))
        {
            throw new ArgumentException("Project ID cannot be null or blank", nameof(projectId));
        }

        return new DeleteProjectCommand(ProjectId.of(projectId));
    }
}
