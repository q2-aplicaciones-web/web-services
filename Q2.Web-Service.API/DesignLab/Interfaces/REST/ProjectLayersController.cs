using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.Queries;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Domain.Services;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST;

[ApiController]
[Route("api/v1/projects")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Project Layers Endpoints")]
public class ProjectLayersController(
    ILayerCommandService layerCommandService,
    ILayerQueryService layerQueryService
) : ControllerBase
{
    /// <summary>
    /// Create a new text layer for a project
    /// </summary>
    /// <param name="projectId">Project ID</param>
    /// <param name="resource">Text layer creation data</param>
    /// <returns>Created text layer</returns>
    [HttpPost("{projectId}/texts")]
    [SwaggerOperation(
        Summary = "Create Text Layer",
        Description = "Creates a new text layer for the project",
        OperationId = "CreateTextLayer")]
    [SwaggerResponse(201, "Text layer created successfully", typeof(LayerResource))]
    [SwaggerResponse(400, "Invalid input data", typeof(ErrorResource))]
    [SwaggerResponse(403, "User not authorized", typeof(ErrorResource))]
    [SwaggerResponse(404, "Project not found", typeof(ErrorResource))]
    [SwaggerResponse(500, "Internal server error", typeof(ErrorResource))]
    public async Task<IActionResult> CreateTextLayer(
        [FromRoute] Guid projectId, 
        [FromBody] CreateTextLayerResource resource)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new ErrorResource(
                    "Invalid input data",
                    string.Join(", ", errors),
                    400,
                    DateTime.UtcNow));
            }

            var createTextLayerCommand = CreateTextLayerCommandFromResource.ToCommandFromResource(resource, projectId);
            var layerId = await layerCommandService.Handle(createTextLayerCommand);

            if (layerId is null)
            {
                return BadRequest(new ErrorResource(
                    "Failed to create text layer",
                    "CREATION_FAILED",
                    400,
                    DateTime.UtcNow));
            }

            // Get the created layer to return full details
            var getLayerByIdQuery = new GetLayerByIdQuery(new LayerId(layerId.Id));
            var layer = layerQueryService.Handle(getLayerByIdQuery);

            if (layer is null)
            {
                return StatusCode(500, new ErrorResource(
                    "Text layer created but could not be retrieved",
                    "RETRIEVAL_FAILED",
                    500,
                    DateTime.UtcNow));
            }

            var layerResource = LayerResourceFromEntityAssembler.FromEntity(layer);
            return CreatedAtAction(nameof(CreateTextLayer), new { projectId, layerId = layerId.Id }, layerResource);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResource(
                "An error occurred while creating the text layer",
                ex.Message,
                500,
                DateTime.UtcNow));
        }
    }

    /// <summary>
    /// Create a new image layer for a project
    /// </summary>
    /// <param name="projectId">Project ID</param>
    /// <param name="resource">Image layer creation data</param>
    /// <returns>Created image layer</returns>
    [HttpPost("{projectId}/images")]
    [SwaggerOperation(
        Summary = "Create Image Layer",
        Description = "Creates a new image layer for the project",
        OperationId = "CreateImageLayer")]
    [SwaggerResponse(201, "Image layer created successfully", typeof(LayerResource))]
    [SwaggerResponse(400, "Invalid input data", typeof(ErrorResource))]
    [SwaggerResponse(403, "User not authorized", typeof(ErrorResource))]
    [SwaggerResponse(404, "Project not found", typeof(ErrorResource))]
    [SwaggerResponse(500, "Internal server error", typeof(ErrorResource))]
    public async Task<IActionResult> CreateImageLayer(
        [FromRoute] Guid projectId, 
        [FromBody] CreateImageLayerResource resource)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new ErrorResource(
                    "Invalid input data",
                    string.Join(", ", errors),
                    400,
                    DateTime.UtcNow));
            }

            // Update resource with project ID from route parameter
            var createImageLayerCommand = CreateImageLayerCommandFromResourceAssembler.ToCommandFromResource(resource, projectId);
            var layerId = await layerCommandService.Handle(createImageLayerCommand);

            if (layerId is null)
            {
                return BadRequest(new ErrorResource(
                    "Failed to create image layer",
                    "CREATION_FAILED",
                    400,
                    DateTime.UtcNow));
            }

            // Get the created layer to return full details
            var getLayerByIdQuery = new GetLayerByIdQuery(new LayerId(layerId.Id));
            var layer = layerQueryService.Handle(getLayerByIdQuery);

            if (layer is null)
            {
                return StatusCode(500, new ErrorResource(
                    "Image layer created but could not be retrieved",
                    "RETRIEVAL_FAILED",
                    500,
                    DateTime.UtcNow));
            }

            var layerResource = LayerResourceFromEntityAssembler.FromEntity(layer);
            return CreatedAtAction(nameof(CreateImageLayer), new { projectId, layerId = layerId.Id }, layerResource);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResource(
                "An error occurred while creating the image layer",
                ex.Message,
                500,
                DateTime.UtcNow));
        }
    }

    /// <summary>
    /// Delete a specific layer from a project
    /// </summary>
    /// <param name="projectId">Project ID</param>
    /// <param name="layerId">Layer ID</param>
    /// <returns>Success message</returns>
    [HttpDelete("{projectId}/layers/{layerId}")]
    [SwaggerOperation(
        Summary = "Delete Layer",
        Description = "Deletes a specific layer from the project",
        OperationId = "DeleteProjectLayer")]
    [SwaggerResponse(200, "Layer deleted successfully", typeof(SuccessMessageResource))]
    [SwaggerResponse(400, "Invalid input data", typeof(ErrorResource))]
    [SwaggerResponse(403, "User not authorized", typeof(ErrorResource))]
    [SwaggerResponse(404, "Layer or project not found", typeof(ErrorResource))]
    [SwaggerResponse(500, "Internal server error", typeof(ErrorResource))]
    public async Task<IActionResult> DeleteProjectLayer(
        [FromRoute] Guid projectId, 
        [FromRoute] Guid layerId)
    {
        try
        {
            if (projectId == Guid.Empty)
            {
                return BadRequest(new ErrorResource(
                    "Invalid project ID format",
                    "INVALID_PROJECT_ID",
                    400,
                    DateTime.UtcNow));
            }

            if (layerId == Guid.Empty)
            {
                return BadRequest(new ErrorResource(
                    "Invalid layer ID format",
                    "INVALID_LAYER_ID",
                    400,
                    DateTime.UtcNow));
            }

            var deleteProjectLayerCommand = new DeleteProjectLayerCommand(projectId.ToString(), layerId.ToString());
            var deletedLayerId = await layerCommandService.Handle(deleteProjectLayerCommand);

            if (deletedLayerId is null)
            {
                return NotFound(new ErrorResource(
                    $"Layer with ID {layerId} not found in project {projectId}",
                    "NOT_FOUND",
                    404,
                    DateTime.UtcNow));
            }

            var successMessage = new SuccessMessageResource(
                $"Layer with ID {layerId} has been successfully deleted from project with ID {projectId}",
                DateTime.UtcNow);

            return Ok(successMessage);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResource(
                "An error occurred while deleting the layer",
                ex.Message,
                500,
                DateTime.UtcNow));
        }
    }

    /// <summary>
    /// Update text layer details
    /// </summary>
    /// <param name="projectId">Project ID</param>
    /// <param name="layerId">Layer ID</param>
    /// <param name="resource">Text layer update data</param>
    /// <returns>Updated text layer</returns>
    [HttpPut("{projectId}/layers/{layerId}/text-details")]
    [SwaggerOperation(
        Summary = "Update Text Layer Details",
        Description = "Updates the text properties of a text layer",
        OperationId = "UpdateTextLayerDetails")]
    [SwaggerResponse(200, "Text layer details updated successfully", typeof(LayerResource))]
    [SwaggerResponse(400, "Invalid input data or layer is not a text layer", typeof(ErrorResource))]
    [SwaggerResponse(404, "Project or layer not found", typeof(ErrorResource))]
    [SwaggerResponse(403, "User not authorized", typeof(ErrorResource))]
    [SwaggerResponse(500, "Internal server error", typeof(ErrorResource))]
    public async Task<IActionResult> UpdateTextLayerDetails(
        [FromRoute] Guid projectId,
        [FromRoute] Guid layerId,
        [FromBody] UpdateTextLayerResource resource)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new ErrorResource(
                    "Invalid input data",
                    string.Join(", ", errors),
                    400,
                    DateTime.UtcNow));
            }

            var updateTextLayerCommand = UpdateTextLayerCommandFromResourceAssembler.ToCommandFromResource(resource, layerId);
            var updatedLayerId = await layerCommandService.Handle(updateTextLayerCommand);

            if (updatedLayerId is null)
            {
                return BadRequest(new ErrorResource(
                    "Failed to update text layer details",
                    "UPDATE_FAILED",
                    400,
                    DateTime.UtcNow));
            }

            // Get the updated layer to return full details
            var getLayerByIdQuery = new GetLayerByIdQuery(new LayerId(updatedLayerId.Id));
            var layer = layerQueryService.Handle(getLayerByIdQuery);

            if (layer is null)
            {
                return StatusCode(500, new ErrorResource(
                    "Text layer updated but could not be retrieved",
                    "RETRIEVAL_FAILED",
                    500,
                    DateTime.UtcNow));
            }

            var layerResource = LayerResourceFromEntityAssembler.FromEntity(layer);
            return Ok(layerResource);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResource(
                "An error occurred while updating the text layer",
                ex.Message,
                500,
                DateTime.UtcNow));
        }
    }

    /// <summary>
    /// Update image layer details
    /// </summary>
    /// <param name="projectId">Project ID</param>
    /// <param name="layerId">Layer ID</param>
    /// <param name="resource">Image layer update data</param>
    /// <returns>Updated image layer</returns>
    [HttpPut("{projectId}/layers/{layerId}/image-details")]
    [SwaggerOperation(
        Summary = "Update Image Layer Details",
        Description = "Updates the image properties of an image layer",
        OperationId = "UpdateImageLayerDetails")]
    [SwaggerResponse(200, "Image layer details updated successfully", typeof(LayerResource))]
    [SwaggerResponse(400, "Invalid input data or layer is not an image layer", typeof(ErrorResource))]
    [SwaggerResponse(404, "Project or layer not found", typeof(ErrorResource))]
    [SwaggerResponse(403, "User not authorized", typeof(ErrorResource))]
    [SwaggerResponse(500, "Internal server error", typeof(ErrorResource))]
    public async Task<IActionResult> UpdateImageLayerDetails(
        [FromRoute] Guid projectId,
        [FromRoute] Guid layerId,
        [FromBody] UpdateImageLayerResource resource)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new ErrorResource(
                    "Invalid input data",
                    string.Join(", ", errors),
                    400,
                    DateTime.UtcNow));
            }

            var updateImageLayerCommand = UpdateImageLayerCommandFromResourceAssembler.ToCommandFromResource(resource, layerId);
            var updatedLayerId = await layerCommandService.Handle(updateImageLayerCommand);

            if (updatedLayerId is null)
            {
                return BadRequest(new ErrorResource(
                    "Failed to update image layer details",
                    "UPDATE_FAILED",
                    400,
                    DateTime.UtcNow));
            }

            // Get the updated layer to return full details
            var getLayerByIdQuery = new GetLayerByIdQuery(new LayerId(updatedLayerId.Id));
            var layer = layerQueryService.Handle(getLayerByIdQuery);

            if (layer is null)
            {
                return StatusCode(500, new ErrorResource(
                    "Image layer updated but could not be retrieved",
                    "RETRIEVAL_FAILED",
                    500,
                    DateTime.UtcNow));
            }

            var layerResource = LayerResourceFromEntityAssembler.FromEntity(layer);
            return Ok(layerResource);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResource(
                "An error occurred while updating the image layer",
                ex.Message,
                500,
                DateTime.UtcNow));
        }
    }

    /// <summary>
    /// Update layer coordinates
    /// </summary>
    /// <param name="projectId">Project ID</param>
    /// <param name="layerId">Layer ID</param>
    /// <param name="resource">Layer coordinates update data</param>
    /// <returns>Updated layer with new coordinates</returns>
    [HttpPut("{projectId}/layers/{layerId}/coordinates")]
    [SwaggerOperation(
        Summary = "Update Layer Coordinates",
        Description = "Updates the coordinates (x, y, z) of a specific layer within a project",
        OperationId = "UpdateLayerCoordinates")]
    [SwaggerResponse(200, "Layer coordinates updated successfully", typeof(LayerResource))]
    [SwaggerResponse(400, "Invalid input data", typeof(ErrorResource))]
    [SwaggerResponse(403, "User not authorized", typeof(ErrorResource))]
    [SwaggerResponse(404, "Project or layer not found", typeof(ErrorResource))]
    [SwaggerResponse(500, "Internal server error", typeof(ErrorResource))]
    public async Task<IActionResult> UpdateLayerCoordinates(
        [FromRoute] [Required] string projectId,
        [FromRoute] [Required] string layerId,
        [FromBody] UpdateLayerCoordinatesResource resource)
    {
        try
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(projectId))
            {
                return BadRequest(new ErrorResource(
                    "Project ID cannot be null or empty",
                    "INVALID_PROJECT_ID",
                    400,
                    DateTime.UtcNow));
            }

            if (string.IsNullOrWhiteSpace(layerId))
            {
                return BadRequest(new ErrorResource(
                    "Layer ID cannot be null or empty",
                    "INVALID_LAYER_ID",
                    400,
                    DateTime.UtcNow));
            }

            if (resource == null)
            {
                return BadRequest(new ErrorResource(
                    "Layer coordinates resource cannot be null",
                    "NULL_RESOURCE",
                    400,
                    DateTime.UtcNow));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new ErrorResource(
                    "Invalid request data",
                    string.Join(", ", errors),
                    400,
                    DateTime.UtcNow));
            }

            // Transform resource to command
            var updateLayerCoordinatesCommand = UpdateLayerCoordinatesCommandFromResourceAssembler
                .ToCommandFromResource(resource, projectId, layerId);

            // Execute command
            var updatedLayerId = await layerCommandService.Handle(updateLayerCoordinatesCommand);

            if (updatedLayerId is null)
            {
                return BadRequest(new ErrorResource(
                    "Failed to update layer coordinates",
                    "UPDATE_FAILED",
                    400,
                    DateTime.UtcNow));
            }

            // Get the updated layer to return full details
            var getLayerByIdQuery = new GetLayerByIdQuery(new LayerId(updatedLayerId.Id));
            var layer = layerQueryService.Handle(getLayerByIdQuery);

            if (layer is null)
            {
                return NotFound(new ErrorResource(
                    $"Updated layer could not be retrieved. Layer ID: {updatedLayerId.Id}",
                    "LAYER_NOT_FOUND_AFTER_UPDATE",
                    404,
                    DateTime.UtcNow));
            }

            var layerResource = LayerResourceFromEntityAssembler.FromEntity(layer);
            return Ok(layerResource);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Project with ID") && ex.Message.Contains("does not exist"))
        {
            return NotFound(new ErrorResource(
                $"Project not found - {ex.Message}",
                "PROJECT_NOT_FOUND",
                404,
                DateTime.UtcNow));
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Layer with ID") && ex.Message.Contains("does not exist"))
        {
            return NotFound(new ErrorResource(
                $"Layer not found - {ex.Message}",
                "LAYER_NOT_FOUND",
                404,
                DateTime.UtcNow));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ErrorResource(
                ex.Message,
                "VALIDATION_ERROR",
                400,
                DateTime.UtcNow));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(new ErrorResource(
                $"User not authorized to update layer coordinates - {ex.Message}",
                "UNAUTHORIZED",
                403,
                DateTime.UtcNow).ToString());
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResource(
                $"Internal server error occurred while updating layer coordinates - {ex.Message}",
                "INTERNAL_SERVER_ERROR",
                500,
                DateTime.UtcNow));
        }
    }
}