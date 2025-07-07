using System;
using System.Collections.Generic;

namespace Q2.Web_Service.API.DesignLab.Interfaces.ACL;

/// <summary>
/// Anti-Corruption Layer for DesignLab context that exposes project information
/// </summary>
public interface IProjectContextFacade
{
    /// <summary>
    /// Verifies if a project exists with the specified ID
    /// </summary>
    bool ProjectExists(Guid projectId);
    
    /// <summary>
    /// Gets the total count of projects for a user
    /// </summary>
    long GetProjectCountByUserId(Guid userId);
    
    /// <summary>
    /// Gets the IDs of all projects belonging to a user
    /// </summary>
    List<Guid> FetchProjectIdsByUserId(Guid userId);
    
    /// <summary>
    /// Gets the details of a specific project for the product context
    /// </summary>
    ProjectDetails? FetchProjectDetailsForProduct(Guid projectId);
}

/// <summary>
/// DTO containing basic project details
/// </summary>
public record ProjectDetails(
    Guid Id,
    string Title,
    string PreviewUrl,
    Guid UserId
);