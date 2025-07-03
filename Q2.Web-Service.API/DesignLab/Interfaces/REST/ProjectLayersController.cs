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
            Console.Out.WriteLine($"Creating text layer for project {projectId} with resource: {resource}");
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

            // Validate required fields
            if (string.IsNullOrWhiteSpace(resource.Text))
            {
                // print the error
                Console.Out.WriteLine("Text cannot be null or empty");
                return BadRequest(new ErrorResource(
                    "Text cannot be null or empty",
                    "INVALID_TEXT",
                    400,
                    DateTime.UtcNow));
            }

            if (string.IsNullOrWhiteSpace(resource.FontColor))
            {
                // print the error
                Console.Out.WriteLine("Font color cannot be null or empty");
                return BadRequest(new ErrorResource(
                    "Font color cannot be null or empty",
                    "INVALID_FONT_COLOR",
                    400,
                    DateTime.UtcNow));
            }

            if (string.IsNullOrWhiteSpace(resource.FontFamily))
            {
                // print the error
                Console.Out.WriteLine("Font family cannot be null or empty");
                
                return BadRequest(new ErrorResource(
                    "Font family cannot be null or empty",
                    "INVALID_FONT_FAMILY",
                    400,
                    DateTime.UtcNow));
            }

            if (resource.FontSize <= 0)
            {
                // print the error
                Console.Out.WriteLine("Font size must be a positive number");
                return BadRequest(new ErrorResource(
                    "Font size must be a positive number",
                    "INVALID_FONT_SIZE",
                    400,
                    DateTime.UtcNow));
            }

            // Set project ID in resource
            var updatedResource = resource with { ProjectId = projectId.ToString() };
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
            if (!layerId.HasValue)
            {
                return StatusCode(500, new ErrorResource(
                    "Layer ID is null after creation",
                    "NULL_LAYER_ID",
                    500,
                    DateTime.UtcNow));
            }
            var getLayerByIdQuery = new GetLayerByIdQuery(layerId.Value); // Use non-nullable Guid
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
            return CreatedAtAction(nameof(CreateTextLayer), new { projectId, layerId }, layerResource);
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

            // Validate required fields
            if (string.IsNullOrWhiteSpace(resource.ImageUrl))
            {
                return BadRequest(new ErrorResource(
                    "Image URL cannot be null or empty",
                    "INVALID_IMAGE_URL",
                    400,
                    DateTime.UtcNow));
            }

            if (string.IsNullOrWhiteSpace(resource.Width))
            {
                return BadRequest(new ErrorResource(
                    "Width cannot be null or empty",
                    "INVALID_WIDTH",
                    400,
                    DateTime.UtcNow));
            }

            if (string.IsNullOrWhiteSpace(resource.Height))
            {
                return BadRequest(new ErrorResource(
                    "Height cannot be null or empty",
                    "INVALID_HEIGHT",
                    400,
                    DateTime.UtcNow));
            }

            // Validate that width and height are positive numbers
            if (!double.TryParse(resource.Width, out var widthValue) || widthValue <= 0)
            {
                return BadRequest(new ErrorResource(
                    "Width must be a positive number",
                    "INVALID_WIDTH_VALUE",
                    400,
                    DateTime.UtcNow));
            }

            if (!double.TryParse(resource.Height, out var heightValue) || heightValue <= 0)
            {
                return BadRequest(new ErrorResource(
                    "Height must be a positive number",
                    "INVALID_HEIGHT_VALUE",
                    400,
                    DateTime.UtcNow));
            }

            var createImageLayerCommand = CreateImageLayerCommandFromResourceAssembler.ToCommandFromResource(resource);
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
            if (!layerId.HasValue)
            {
                return StatusCode(500, new ErrorResource(
                    "Layer ID is null after creation",
                    "NULL_LAYER_ID",
                    500,
                    DateTime.UtcNow));
            }
            var getLayerByIdQuery = new GetLayerByIdQuery(layerId.Value); // Use non-nullable Guid
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
            return CreatedAtAction(nameof(CreateImageLayer), new { projectId, layerId }, layerResource);
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