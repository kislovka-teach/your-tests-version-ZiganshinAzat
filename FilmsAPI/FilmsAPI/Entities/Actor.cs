using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Entities;

public class Actor
{
    [Key]
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Biography { get; set; }

    public List<Film> Films { get; set; } = new();
}