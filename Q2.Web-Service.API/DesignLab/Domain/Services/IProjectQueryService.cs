using Q2.Web_Service.API.DesignLab.Domain.Model.Aggregates;
using Q2.Web_Service.API.DesignLab.Domain.Model.Queries;

namespace Q2.Web_Service.API.DesignLab.Domain.Services;

public interface IProjectQueryService
{
    Task<IEnumerable<Project>> Handle(GetProjectsByUserIdQuery query);
    Task<Project?> Handle(GetProjectByIdQuery query);
}