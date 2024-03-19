using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Abstractions;

public interface IIssueRepository
{
    Task<List<Issue>> GetAllIssuesOfProject(Project project);
    
    Task<Issue> Add(Issue issue);
    
    void GiveIssueForUser(Issue issue, User user);
    
    Task<List<Issue>> GetAllUserIssues(User user);
}