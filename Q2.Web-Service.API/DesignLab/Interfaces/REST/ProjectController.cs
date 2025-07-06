using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.DesignLab.Domain.Model.Queries;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Domain.Services;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;
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
    /// <summary>
    /// Get all projects for a specific user
    /// </summary>
    /// <param name="userId">User ID to filter projects (required)</param>
    /// <returns>List of user projects</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get Projects by User",
        Description = "Gets all projects for a specific user",
        OperationId = "GetProjectsByUser")]
    [SwaggerResponse(200, "Projects found successfully", typeof(List<ProjectResource>))]
    [SwaggerResponse(400, "User ID is required", typeof(ErrorResource))]
    [SwaggerResponse(404, "No projects found for user", typeof(ErrorResource))]
    public async Task<IActionResult> GetProjects([FromQuery] Guid? userId)
    {
        try
        {
            if (!userId.HasValue)
            {
                return BadRequest(new ErrorResource(
                    "User ID is required as query parameter",
                    "MISSING_USER_ID",
                    400,
                    DateTime.UtcNow));
            }

            var getProjectsByUserIdQuery = new GetProjectsByUserIdQuery(userId.Value);
            var projects = (await projectQueryService.Handle(getProjectsByUserIdQuery)).ToList();

            if (!projects.Any())
            {
                return NotFound(new ErrorResource(
                    $"No projects found for user with ID {userId}",
                    "NOT_FOUND",
                    404,
                    DateTime.UtcNow));
            }

            var projectsResources = projects.Select(ProjectResourceFromEntityAssembler.toResourceFromEntity).ToList();
            return Ok(projectsResources);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResource(
                "An error occurred while retrieving projects",
                ex.Message,
                500,
                DateTime.UtcNow));
        }
    }

    /// <summary>
    /// Get a specific project by ID
    /// </summary>
    /// <param name="projectId">Project ID</param>
    /// <returns>Project details</returns>
    [HttpGet("{projectId}")]
    [SwaggerOperation(
        Summary = "Get Project by ID",
        Description = "Gets a specific project by its ID",
        OperationId = "GetProjectById")]
    [SwaggerResponse(200, "Project found successfully", typeof(ProjectResource))]
    [SwaggerResponse(400, "Invalid project ID format", typeof(ErrorResource))]
    [SwaggerResponse(404, "Project not found", typeof(ErrorResource))]
    public async Task<IActionResult> GetProjectById([FromRoute] Guid projectId)
    {
        try
        {
            if (projectId == Guid.Empty)
            {
                return BadRequest(new ErrorResource(
                    "Invalid project ID format",
                    "INVALID_ID",
                    400,
                    DateTime.UtcNow));
            }

            var getProjectByIdQuery = new GetProjectByIdQuery(projectId);
            var project = await projectQueryService.Handle(getProjectByIdQuery);

            if (project is null)
            {
                return NotFound(new ErrorResource(
                    $"Project with ID {projectId} not found",
                    "NOT_FOUND",
                    404,
                    DateTime.UtcNow));
            }

            var projectResource = ProjectResourceFromEntityAssembler.toResourceFromEntity(project);
            return Ok(projectResource);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResource(
                "An error occurred while retrieving the project",
                ex.Message,
                500,
                DateTime.UtcNow));
        }
    }

    /// <summary>
    /// Create a new design project
    /// </summary>
    /// <param name="resource">Project creation data</param>
    /// <returns>Created project</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create Project",
        Description = "Creates a new design project",
        OperationId = "CreateProject")]
    [SwaggerResponse(201, "Project created successfully", typeof(ProjectResource))]
    [SwaggerResponse(400, "Invalid input data", typeof(ErrorResource))]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectResource resource)
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

            try
            {
                var createProjectCommand = CreateProjectCommandFromResourceAssembler.ToCommandFromResource(resource);
                var projectId = await projectCommandService.Handle(createProjectCommand);

                if (projectId is null)
                {
                    return BadRequest(new ErrorResource(
                        "Failed to create project",
                        "CREATION_FAILED",
                        400,
                        DateTime.UtcNow));
                }

                var getProjectByIdQuery = new GetProjectByIdQuery(projectId.Id);
                var project = await projectQueryService.Handle(getProjectByIdQuery);

                if (project is null)
                {
                    return StatusCode(500, new ErrorResource(
                        "Project created but could not be retrieved",
                        "RETRIEVAL_FAILED",
                        500,
                        DateTime.UtcNow));
                }

                var projectResource = ProjectResourceFromEntityAssembler.toResourceFromEntity(project);
                return CreatedAtAction(nameof(GetProjectById), new { projectId = projectId.Id }, projectResource);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(new ErrorResource(
                    "Invalid enum value provided",
                    argEx.Message,
                    400,
                    DateTime.UtcNow));
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResource(
                "An error occurred while creating the project",
                ex.Message,
                500,
                DateTime.UtcNow));
        }
    }

    /// <summary>
    /// Update product details of a specific project
    /// </summary>
    /// <param name="projectId">Project ID</param>
    /// <param name="resource">Product details to update</param>
    /// <returns>Success message</returns>
    [HttpPut("{projectId}/details")]
    [SwaggerOperation(
        Summary = "Update Product Details",
        Description = "Update the product details of a project",
        OperationId = "UpdateProductDetails")]
    [SwaggerResponse(200, "Product details updated successfully", typeof(SuccessMessageResource))]
    [SwaggerResponse(400, "Invalid input data", typeof(ErrorResource))]
    [SwaggerResponse(404, "Project not found", typeof(ErrorResource))]
    public async Task<IActionResult> UpdateProductDetails(
        [FromRoute] Guid projectId,
        [FromBody] UpdateProductDetailsResource resource)
    {
        try
        {
            if (projectId == Guid.Empty)
            {
                return BadRequest(new ErrorResource(
                    "Project ID cannot be null or empty",
                    "INVALID_PROJECT_ID",
                    400,
                    DateTime.UtcNow));
            }

            if (resource == null)
            {
                return BadRequest(new ErrorResource(
                    "Product details resource cannot be null",
                    "INVALID_RESOURCE",
                    400,
                    DateTime.UtcNow));
            }

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

            var command = UpdateProductDetailsCommandFromResourceAssembler.ToCommandFromResource(resource, projectId.ToString());
            var updatedProjectId = await projectCommandService.Handle(command);

            if (updatedProjectId == null)
            {
                return BadRequest(new ErrorResource(
                    "Failed to update product details",
                    "UPDATE_FAILED",
                    400,
                    DateTime.UtcNow));
            }

            return Ok(new SuccessMessageResource(
                $"Product details updated for project with ID: {updatedProjectId.Id}",
                DateTime.UtcNow));
        }
        catch (ArgumentException ex)
        {
            if (ex.Message.Contains("does not exist"))
            {
                return NotFound(new ErrorResource(
                    $"Project not found - {ex.Message}",
                    "PROJECT_NOT_FOUND",
                    404,
                    DateTime.UtcNow));
            }

            return BadRequest(new ErrorResource(
                $"Invalid request data - {ex.Message}",
                "INVALID_DATA",
                400,
                DateTime.UtcNow));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResource(
                $"Internal server error occurred while updating product details - {ex.Message}",
                "INTERNAL_ERROR",
                500,
                DateTime.UtcNow));
        }
    }

    /// <summary>
    /// Delete a project by its ID
    /// </summary>
    /// <param name="projectId">Project ID</param>
    /// <returns>Success message</returns>
    [HttpDelete("{projectId}")]
    [SwaggerOperation(
        Summary = "Delete Project",
        Description = "Delete a project by its ID",
        OperationId = "DeleteProject")]
    [SwaggerResponse(200, "Project deleted successfully", typeof(SuccessMessageResource))]
    [SwaggerResponse(400, "Invalid project ID format", typeof(ErrorResource))]
    [SwaggerResponse(404, "Project not found", typeof(ErrorResource))]
    [SwaggerResponse(500, "Internal server error", typeof(ErrorResource))]
    public async Task<IActionResult> DeleteProject([FromRoute] Guid projectId)
    {
        try
        {
            if (projectId == Guid.Empty)
            {
                return BadRequest(new ErrorResource(
                    "Project ID cannot be null or empty",
                    "INVALID_PROJECT_ID",
                    400,
                    DateTime.UtcNow));
            }

            var deleteProjectCommand = DeleteProjectCommandFromResourceAssembler.ToCommandFromResource(projectId.ToString());
            await projectCommandService.Handle(deleteProjectCommand);

            return Ok(new SuccessMessageResource(
                $"Project with ID {projectId} has been successfully deleted",
                DateTime.UtcNow));
        }
        catch (ArgumentException ex)
        {
            if (ex.Message.Contains("does not exist"))
            {
                return NotFound(new ErrorResource(
                    $"Project not found with ID: {projectId}",
                    "PROJECT_NOT_FOUND",
                    404,
                    DateTime.UtcNow));
            }

            return BadRequest(new ErrorResource(
                $"Invalid project ID format: {projectId}",
                "INVALID_PROJECT_ID",
                400,
                DateTime.UtcNow));
        }
        catch (InvalidOperationException ex)
        {
            if (ex.Message.Contains("Failed to delete project"))
            {
                return StatusCode(500, new ErrorResource(
                    $"Failed to delete project: {ex.Message}",
                    "DELETE_FAILED",
                    500,
                    DateTime.UtcNow));
            }

            return StatusCode(500, new ErrorResource(
                $"Error deleting project: {ex.Message}",
                "DELETE_ERROR",
                500,
                DateTime.UtcNow));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResource(
                $"Internal server error occurred while deleting project - {ex.Message}",
                "INTERNAL_ERROR",
                500,
                DateTime.UtcNow));
        }
    }
}