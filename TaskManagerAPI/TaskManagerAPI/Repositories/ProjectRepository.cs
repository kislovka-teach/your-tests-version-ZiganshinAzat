using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Abstractions;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Repositories;

public class ProjectRepository: IProjectRepository
{
    private readonly AppDbContext appDbContext;

    public ProjectRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }
    public async Task<Project> Add(Project project)
    {
        var result = await appDbContext.Projects.AddAsync(project);
        return result.Entity;
    }
    
    public Task<List<Project>> GetAllProjects()
    {
        return appDbContext.Projects.ToListAsync();
    }
    
    public async Task<List<Project>> GetAllProjectsByUser(String userLogin)
    {
        var issues = await appDbContext.Issues
            .Where(i => i.UserLogin == userLogin)
            .ToListAsync();

        var projectIds = issues.Select(i => i.ProjectId).Distinct();
        var projects = await appDbContext.Projects
            .Where(p => projectIds.Contains(p.ProjectId))
            .ToListAsync();

        return projects;
    }
}