using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.DesignLab.Domain.Model.Queries;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Domain.Services;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST;

[ApiController]
[Route("api/v1/projects")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Project Endpoints")]
public class ProjectController(
    IProjectCommandService projectCommandService,
    IProjectQueryService projectQueryService) : ControllerBase
{

    [HttpGet("users/{userId")]
    public async Task<IActionResult> GetProjectsByUserId([FromRoute] Guid userId)
    {
        {
            var getProjectsByUserIdQuery = new GetProjectsByUserIdQuery(userId);
            var projects = await projectQueryService.Handle(getProjectsByUserIdQuery);
            if (projects is null || !projects.Any())
            {
                return NotFound($"No projects found for user with ID {userId}.");
            }

            // Convert to 
            var projectsResources = projects.Select(ProjectResourceFromEntityAssembler.toResourceFromEntity).ToList();
            
            return Ok(projectsResources);
        }
    }
    
}