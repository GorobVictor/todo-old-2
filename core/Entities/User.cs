using core.Entities.Base;
using core.Enums;

namespace core.Entities;

public class User : FullEntity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    
    public string FullName { get => $"{this.Name} {this.Surname}"; }
    
    public string? Email { get; set; }
    
    public string? Phone { get; set; }
    
    public string? Password { get; set; }
    
    public UserRole Role { get; set; }
}