using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Entities;

public class Film
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ReleaseYear { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public List<Actor> Actors { get; set; } = new();
}