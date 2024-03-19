using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Abstractions;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Repositories;

public class ProjectRepository(AppDbContext appDbContext): IProjectRepository
{
    public async Task<Project> Add(Project project)
    {
        var result = await appDbContext.Projects.AddAsync(project);
        return result.Entity;
    }
    
    public Task<List<Project>> GetAllProjects()
    {
        return appDbContext.Projects.ToListAsync();
    }
    
    public async Task<List<Project>> GetAllProjectsByUser(User user)
    {
        var issues = await appDbContext.Issues
            .Where(i => i.UserLogin == user.Login)
            .ToListAsync();

        var projectIds = issues.Select(i => i.ProjectId).Distinct();
        var projects = await appDbContext.Projects
            .Where(p => projectIds.Contains(p.ProjectId))
            .ToListAsync();

        return projects;
    }
}