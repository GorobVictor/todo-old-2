using core.Enums;

namespace core.DTO.UserDTO;

public class UserDto
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Surname { get; set; }
    
    public string? FullName { get; set; }
    
    public string? Email { get; set; }
    
    public string? Phone { get; set; }
    
    public UserRole Role { get; set; }
    
    public string RoleName { get => Role.ToString(); }
}