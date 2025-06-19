using Q2.Web_Service.API.DesignLab.Domain.Model.Aggregates;
using Q2.Web_Service.API.DesignLab.Domain.Model.Queries;
using Q2.Web_Service.API.DesignLab.Domain.Repositories;
using Q2.Web_Service.API.DesignLab.Domain.Services;

namespace Q2.Web_Service.API.DesignLab.Application.Internal.QueryServices;

public class ProjectQueryService(IProjectRepository projectRepository): IProjectQueryService
{

    public async Task<IEnumerable<Project>> Handle(GetProjectsByUserIdQuery query)
    {
        
        return await projectRepository.GetAllProjectsByUserIdAsync(query.UserId);
    }

    public async Task<Project?> Handle(GetProjectByIdQuery query)
    {
        return await projectRepository.GetProjectByIdAsync(query.ProjectId);
    }
    
}