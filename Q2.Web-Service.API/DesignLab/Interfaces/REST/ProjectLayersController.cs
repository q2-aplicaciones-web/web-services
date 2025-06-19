using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.Queries;
using Q2.Web_Service.API.DesignLab.Domain.Services;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST;

[ApiController]
[Route("api/v1/projects/layers")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Project Endpoints")]
public class ProjectLayersController(
    ILayerCommandService layerCommandService
) : ControllerBase
{
    
    [HttpPost("texts")]
    public async Task<IActionResult> CreateTextLayer([FromBody] CreateTextLayerResource resource)
    {
        var createTextLayerCommand = CreateTextLayerCommandFromResource.ToCommandFromResource(resource);
        
        var layerId = await layerCommandService.Handle(createTextLayerCommand);
        
        if (layerId is null)
        {
            return BadRequest("Failed to create text layer.");
        }
            
        return CreatedAtAction(nameof(CreateTextLayer), new { layerId }, resource);
    }

    [HttpPost("images")]
    public async Task<IActionResult> CreateImageLayer([FromBody] CreateImageLayerResource command)
    {
        var createImageLayerCommand = CreateImageLayerCommandFromResourceAssembler.ToCommandFromResource(command);
        var layerId = await layerCommandService.Handle(createImageLayerCommand);
        if (layerId is null)
        {
            return BadRequest("Failed to create image layer.");
        }
        return CreatedAtAction(nameof(CreateImageLayer), new { layerId }, command);
    }
    
    [HttpDelete("{projectId}/layers/{layerId}")]
    public async Task<IActionResult> DeleteProjectLayer([FromRoute] string projectId, [FromRoute] string layerId)
    {
        var deleteProjectLayerCommand = new DeleteProjectLayerCommand(projectId, layerId);
        var deletedLayerId = await layerCommandService.Handle(deleteProjectLayerCommand);
        
        if (deletedLayerId is null)
        {
            return NotFound($"Layer with ID {layerId} not found in project {projectId}.");
        }
        
        // return a message indicating successful deletion
        return NoContent();
    }
    
}   