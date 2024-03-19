using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerAPI.Entities;

public class Issue
{
    [Key]
    public Guid IssueId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public IssuePriority Priority { get; set; }
    public IssueStatus Status { get; set; }

    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    public string UserLogin { get; set; }
    public User User { get; set; }
}