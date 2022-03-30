using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using AutoMapper;
using core.DTO.UserDTO;
using core.Entities;
using core.Enums;
using core.Ex;
using core.Interfaces;
using infrastructure.Statics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace infrastructure.Services;

public class UserService : IUserService
{
    private IUserRepository UserRepo { get; set; }
    private IMapper Mapper { get; set; }
    
    public UserService(IUserRepository userRepo, IMapper mapper)
    {
        this.UserRepo = userRepo;
        this.Mapper = mapper;
    }

    public async Task<UserDto> SignUp(UserSignUp userDto)
    {
        var user = this.Mapper.Map<User>(userDto);
        user.Role = UserRole.Member;
        
        var result = await this.UserRepo.AddAsync(user);

        return this.Mapper.Map<UserDto>(result);
    }
    
    public async Task<ResultLoginDto> GetIdentityAsync(LoginDto user)
    {
        var User = await UserRepo.Get(x=>x.Email == user.Email && x.Password == user.Password).FirstOrDefaultAsync();

        if (User != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, User.FullName),
                new Claim("userId", User.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, User.Role.ToString())
            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            if (claimsIdentity == null)
            {
                throw new Exception("Invalid username or password.");
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromHours(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new ResultLoginDto(encodedJwt, this.Mapper.Map<UserDto>(User));
        }

        throw new FriendlyException("invalid login", HttpStatusCode.NotFound);
    }
}