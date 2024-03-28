using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Repositories;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllCommentsOfIssue(Guid issueId);
    
    Task<Comment> AddComment(Comment comment);
    
    void UpdateComment(Comment comment, string newText);
    
    void RemoveComment(Guid commentId);
}