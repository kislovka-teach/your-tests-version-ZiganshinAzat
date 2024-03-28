using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Abstractions;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Repositories;

public class IssueRepository(AppDbContext appDbContext): IIssueRepository
{
    public async Task<List<Issue>> GetAllIssuesOfProject(Guid projectId)
    {
        var issues = await appDbContext.Issues
            .Where(issue => issue.ProjectId == projectId)
            .ToListAsync();

        return issues;
    }

    public async Task<Issue> Add(Issue issue)
    {
        var result = await appDbContext.Issues.AddAsync(issue);
        return result.Entity;
    }

    public void GiveIssueForUser(Issue issue, User user)
    {
        issue.UserLogin = user.Login;
        appDbContext.Issues.Update(issue);
    }

    public async Task<List<Issue>> GetAllUserIssues(String userLogin)
    {
        var issues = await appDbContext.Issues
            .Where(issue => issue.UserLogin == userLogin)
            .ToListAsync();

        return issues;
    }
}