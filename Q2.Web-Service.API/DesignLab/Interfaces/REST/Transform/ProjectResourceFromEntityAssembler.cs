using Q2.Web_Service.API.DesignLab.Domain.Model.Aggregates;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public class ProjectResourceFromEntityAssembler
{
    public static ProjectResource toResourceFromEntity(Project project)
    {
        var layers = project.Layers.Select(LayerResourceFromEntityAssembler.FromEntity).ToList();
        return new ProjectResource
        (
            project.Id.Id,
            project.Title,
            project.UserId.Id,
            project.PreviewUrl.ToString(),
            project.Status,
            project.Color,
            project.Size,
            project.Gender,
            layers,
            project.CreatedAt.ToString(),
            project.UpdatedAt.ToString()
        );
    }
}