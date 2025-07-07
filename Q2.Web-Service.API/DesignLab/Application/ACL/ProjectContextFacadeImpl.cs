using System;
using System.Collections.Generic;
using System.Linq;
using Q2.Web_Service.API.DesignLab.Domain.Model.Queries;
using Q2.Web_Service.API.DesignLab.Domain.Services;
using Q2.Web_Service.API.DesignLab.Interfaces.ACL;

namespace Q2.Web_Service.API.DesignLab.Application.ACL;

/// <summary>
/// Implementation of the facade for DesignLab context
/// </summary>
public class ProjectContextFacadeImpl : IProjectContextFacade
{
    private readonly IProjectQueryService _projectQueryService;
    
    public ProjectContextFacadeImpl(IProjectQueryService projectQueryService)
    {
        _projectQueryService = projectQueryService ?? throw new ArgumentNullException(nameof(projectQueryService));
    }

    public bool ProjectExists(Guid projectId)
    {
        try
        {
            var query = new GetProjectByIdQuery(projectId);
            var project = _projectQueryService.Handle(query).GetAwaiter().GetResult();
            return project != null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public long GetProjectCountByUserId(Guid userId)
    {
        try
        {
            var query = new GetProjectsByUserIdQuery(userId);
            var projects = _projectQueryService.Handle(query).GetAwaiter().GetResult();
            return projects.Count();
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public List<Guid> FetchProjectIdsByUserId(Guid userId)
    {
        try
        {
            var query = new GetProjectsByUserIdQuery(userId);
            var projects = _projectQueryService.Handle(query).GetAwaiter().GetResult();
            return projects.Select(p => p.Id.Id).ToList();
        }
        catch (Exception)
        {
            return new List<Guid>();
        }
    }

    public ProjectDetails? FetchProjectDetailsForProduct(Guid projectId)
    {
        try
        {
            var query = new GetProjectByIdQuery(projectId);
            var project = _projectQueryService.Handle(query).GetAwaiter().GetResult();
            if (project == null) return null;
            
            return new ProjectDetails(
                project.Id.Id,
                project.Title,
                project.PreviewUrl?.ToString() ?? string.Empty,
                project.UserId.Id
            );
        }
        catch (Exception)
        {
            return null;
        }
    }
}