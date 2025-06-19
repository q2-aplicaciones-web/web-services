using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Services;

public interface IProjectCommandService
{
    Task<ProjectId?> Handle(CreateProjectCommand command);
}