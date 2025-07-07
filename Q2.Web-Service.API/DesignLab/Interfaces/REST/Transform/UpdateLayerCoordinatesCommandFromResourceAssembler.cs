using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public static class UpdateLayerCoordinatesCommandFromResourceAssembler
{
    public static UpdateLayerCoordinatesCommand ToCommandFromResource(
        UpdateLayerCoordinatesResource resource, 
        string projectId, 
        string layerId)
    {
        if (resource == null)
        {
            throw new ArgumentNullException(nameof(resource), "Layer coordinates resource cannot be null");
        }

        if (string.IsNullOrWhiteSpace(projectId))
        {
            throw new ArgumentException("Project ID cannot be null or empty", nameof(projectId));
        }

        if (string.IsNullOrWhiteSpace(layerId))
        {
            throw new ArgumentException("Layer ID cannot be null or empty", nameof(layerId));
        }

        // Parse and validate GUIDs
        if (!Guid.TryParse(projectId, out var projectGuid))
        {
            throw new ArgumentException("Invalid Project ID format", nameof(projectId));
        }

        if (!Guid.TryParse(layerId, out var layerGuid))
        {
            throw new ArgumentException("Invalid Layer ID format", nameof(layerId));
        }

        return new UpdateLayerCoordinatesCommand(
            new ProjectId(projectGuid),
            new LayerId(layerGuid),
            resource.X,
            resource.Y,
            resource.Z
        );
    }
}
