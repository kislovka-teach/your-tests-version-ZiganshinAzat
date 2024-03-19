using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Repositories;

public class CommentRepository(AppDbContext appDbContext): ICommentRepository
{
    public async Task<List<Comment>> GetAllCommentsOfIssue(Guid issueId)
    {
        var comments = await appDbContext.Comments
            .Where(comment => comment.IssueId == issueId)
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

    public async void RemoveComment(Guid commentId)
    {
        appDbContext.Comments.Remove(await GetCommentById(commentId));
    }
    
    public async Task<Comment> GetCommentById(Guid commentId)
    {
        var comment = await appDbContext.Comments.FindAsync(commentId);

        return comment;
    }
}