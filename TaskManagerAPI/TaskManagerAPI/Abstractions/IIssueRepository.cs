using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Abstractions;

public interface IIssueRepository
{
    Task<List<Issue>> GetAllIssuesOfProject(Guid projectId);
    
    Task<Issue> Add(Issue issue);
    
    void GiveIssueForUser(Issue issue, User user);
    
    Task<List<Issue>> GetAllUserIssues(String userLogin);
}