using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Entities;

public class Project
{
    [Key]
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public ProjectStatus Status { get; set; }
}