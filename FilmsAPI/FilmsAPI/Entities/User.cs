using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Entities;

public class User
{
    [Key]
    public string Login { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}