using AutoMapper;
using core.Entities;
using core.Interfaces;

namespace infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ToDoContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}