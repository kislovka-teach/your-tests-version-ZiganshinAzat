using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Repositories;

public class CommentRepository(AppDbContext appDbContext): ICommentRepository
{
    public async Task<List<Comment>> GetAllCommentsOfIssue(Issue issue)
    {
        var comments = await appDbContext.Comments
            .Where(comment => comment.IssueId == issue.IssueId)
            .ToListAsync();

        return comments;
    }

    public async Task<Comment> AddComment(Comment comment)
    {
        var result = await appDbContext.Comments.AddAsync(comment);
        
        return result.Entity;
    }

    public void UpdateComment(Comment comment, string newText)
    {
        comment.Text = newText;
        appDbContext.Comments.Update(comment);
    }

    public void RemoveComment(Comment comment)
    {
        appDbContext.Comments.Remove(comment);
    }
}