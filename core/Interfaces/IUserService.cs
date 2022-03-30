using core.DTO.UserDTO;

namespace core.Interfaces;

public interface IUserService
{
    Task<UserDto> SignUp(UserSignUp user);
    
    Task<ResultLoginDto> GetIdentityAsync(LoginDto user);
}