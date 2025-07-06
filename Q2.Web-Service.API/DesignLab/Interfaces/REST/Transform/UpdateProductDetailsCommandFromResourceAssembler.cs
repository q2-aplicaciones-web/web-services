using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public static class UpdateProductDetailsCommandFromResourceAssembler
{
    public static UpdateProjectDetailsCommand ToCommandFromResource(UpdateProductDetailsResource resource, string projectId)
    {
        if (resource == null)
        {
            throw new ArgumentNullException(nameof(resource));
        }
        
        if (string.IsNullOrWhiteSpace(projectId))
        {
            throw new ArgumentException("Project ID cannot be null or empty", nameof(projectId));
        }

        Uri? previewUrl = null;
        if (!string.IsNullOrWhiteSpace(resource.PreviewUrl))
        {
            if (!Uri.TryCreate(resource.PreviewUrl, UriKind.Absolute, out previewUrl))
            {
                throw new ArgumentException("Invalid preview URL format");
            }
        }

        return new UpdateProjectDetailsCommand(
            ProjectId.of(projectId),
            previewUrl,
            Enum.Parse<EProjectStatus>(resource.Status, true),
            Enum.Parse<EGarmentColor>(resource.GarmentColor, true),
            Enum.Parse<EGarmentSize>(resource.GarmentSize, true),
            Enum.Parse<EGarmentGender>(resource.GarmentGender, true)
        );
    }
}
