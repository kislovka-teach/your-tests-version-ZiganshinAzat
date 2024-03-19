using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Repositories;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllCommentsOfIssue(Issue issue);
    
    Task<Comment> AddComment(Comment comment);
    
    void UpdateComment(Comment comment, string newText);
    
    void RemoveComment(Comment comment);
}