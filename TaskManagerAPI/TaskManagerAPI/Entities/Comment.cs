using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerAPI.Entities;

public class Comment
{
    [Key]
    public Guid CommentId { get; set; }

    public string Text { get; set; }
    public DateTime CreationDate { get; set; }

    public Guid IssueId { get; set; }
    public Issue Issue { get; set; }

    public String UserLogin { get; set; }
    public User User { get; set; }
}