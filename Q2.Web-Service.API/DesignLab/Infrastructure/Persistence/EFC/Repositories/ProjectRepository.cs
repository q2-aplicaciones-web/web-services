using Q2.Web_Service.API.DesignLab.Domain.Model.Aggregates;
using Q2.Web_Service.API.DesignLab.Domain.Repositories;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Infrastructure.Persistence.EFC.Repositories;

public class ProjectRepository(AppDbContext context) : BaseRepository<Project>(context), IProjectRepository
{
    public async Task<IEnumerable<Project>> GetAllProjectsByUserIdAsync(Guid userId)
    {
        return await Context
            .Set<Project>()
            .Include(project => project.Layers)
            .Where(project => project.UserId == new UserId(userId)).ToListAsync();
    }

    public async Task<Project?> GetProjectByIdAsync(Guid projectId)
    {
        return await Context
            .Set<Project>()
            .Include(project => project.Layers)
            .FirstOrDefaultAsync(project => project.Id == new ProjectId(projectId));
    }

    public async Task<Project?> FindByIdAsync(ProjectId projectId)
    {
        return await Context
            .Set<Project>()
            .Include(project => project.Layers)
            .FirstOrDefaultAsync(project => project.Id == projectId);
    }
}