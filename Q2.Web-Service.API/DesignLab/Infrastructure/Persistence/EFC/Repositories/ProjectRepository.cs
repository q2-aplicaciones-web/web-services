using Q2.Web_Service.API.DesignLab.Domain.Model.Aggregates;
using Q2.Web_Service.API.DesignLab.Domain.Repositories;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Q2.Web_Service.API.DesignLab.Infrastructure.Persistence.EFC.Repositories;

public class ProjectRepository(AppDbContext context) : BaseRepository<Project>(context), IProjectRepository
{
    public async Task<IEnumerable<Project>> GetAllProjectsByUserIdAsync(Guid userId)
    {
        return await Context
            .Set<Project>()
            .Include(project => project.UserId)
            .Where(project => project.UserId.Id == userId).ToListAsync();
    }

    public async Task<Project?> GetProjectByIdAsync(Guid projectId)
    {
        return await Context
            .Set<Project>()
            .Include(project => project.UserId)
            .FirstOrDefaultAsync(project => project.Id.Id == projectId);
    }
}