using Q2.Web_Service.API.DesignLab.Domain.Model.Aggregates;
using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.Entities;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Domain.Repositories;
using Q2.Web_Service.API.DesignLab.Domain.Services;
using Q2.Web_Service.API.Shared.Domain.Repositories;

namespace Q2.Web_Service.API.DesignLab.Application.Internal.CommandServices;

public class ProjectCommandService(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    : IProjectCommandService
{
 
    public async Task<ProjectId?> Handle(CreateProjectCommand command)
    {
        var project = new Project(command);
        await projectRepository.AddAsync(project);
        await unitOfWork.CompleteAsync();

        // Here should be executed all events

        return project.Id;
    }
  
    public async Task<ProjectId?> Handle(UpdateProjectDetailsCommand command)
    {
        var project = await projectRepository.FindByIdAsync(command.ProjectId);
        if (project is null)
        {
            throw new ArgumentException($"Project with ID {command.ProjectId.Id} does not exist.");
        }

        project.UpdateDetails(command.PreviewUrl, command.Status, command.GarmentColor, command.GarmentSize, command.GarmentGender);
        projectRepository.Update(project);
        await unitOfWork.CompleteAsync();

        return project.Id;
    }

    public async Task Handle(DeleteProjectCommand command)
    {
        var project = await projectRepository.FindByIdAsync(command.ProjectId);
        if (project is null)
        {
            throw new ArgumentException($"Project with ID {command.ProjectId.Id} does not exist.");
        }

        try
        {
            projectRepository.Remove(project);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to delete project with ID {command.ProjectId.Id}", ex);
        }
    }
}