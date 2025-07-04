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

            // Update resource with project ID from route parameter
            var updatedResource = resource with { ProjectId = projectId };
            var createTextLayerCommand = CreateTextLayerCommandFromResource.ToCommandFromResource(updatedResource);
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
            var updatedResource = resource with { ProjectId = projectId };
            var createImageLayerCommand = CreateImageLayerCommandFromResourceAssembler.ToCommandFromResource(updatedResource);
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
}   