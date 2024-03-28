using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Entities;

public class User
{
    [Key]
    public string Login { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}