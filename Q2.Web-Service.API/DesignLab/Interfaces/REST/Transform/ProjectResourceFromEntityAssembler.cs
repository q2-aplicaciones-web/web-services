using Q2.Web_Service.API.DesignLab.Domain.Model.Aggregates;
using Q2.Web_Service.API.DesignLab.Domain.Model.Entities;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public class ProjectResourceFromEntityAssembler
{
    public static ProjectResource toResourceFromEntity(Project project)
    {
        var layers = (project.Layers ?? (ICollection<Layer>)new List<Layer>())
            .Select(LayerResourceFromEntityAssembler.FromEntity)
            .ToList();
            
        return new ProjectResource
        (
            project.Id.Id,
            project.Title,
            project.UserId.Id,
            project.PreviewUrl?.ToString() ?? string.Empty,
            project.Status.ToString(),
            project.Color.ToString(),
            project.Size.ToString(),
            project.Gender.ToString(),
            layers,
            project.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            project.UpdatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
        );
    }
}