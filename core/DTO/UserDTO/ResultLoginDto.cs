namespace core.DTO.UserDTO;

public class ResultLoginDto
{
    public ResultLoginDto(string? token, UserDto user)
    {
        Token = token;
        User = user;
    }

    public string? Token { get; set; }
    
    public UserDto User { get; set; }
}