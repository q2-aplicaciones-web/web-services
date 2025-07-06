using System;
using System.Collections.Generic;

namespace Q2.Web_Service.API.DesignLab.Interfaces.ACL;

/// <summary>
/// Anti-Corruption Layer para el contexto de DesignLab que expone información de proyectos
/// </summary>
public interface IProjectContextFacade
{
    /// <summary>
    /// Verifica si existe un proyecto con el ID especificado
    /// </summary>
    bool ProjectExists(Guid projectId);
    
    /// <summary>
    /// Obtiene el conteo total de proyectos para un usuario
    /// </summary>
    long GetProjectCountByUserId(Guid userId);
    
    /// <summary>
    /// Obtiene los IDs de todos los proyectos de un usuario
    /// </summary>
    List<Guid> FetchProjectIdsByUserId(Guid userId);
    
    /// <summary>
    /// Obtiene los detalles de un proyecto específico para el contexto de productos
    /// </summary>
    ProjectDetails? FetchProjectDetailsForProduct(Guid projectId);
}

/// <summary>
/// DTO que contiene los detalles básicos de un proyecto
/// </summary>
public record ProjectDetails(
    Guid Id,
    string Title,
    string PreviewUrl,
    Guid UserId
);