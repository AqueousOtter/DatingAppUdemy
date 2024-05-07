using API.Data;
using API.Entities;
using API.Interfaces;

namespace API;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext _context)
    {
        this._context = _context;
    }
    public Task<AppUser> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> GetUserByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveAllAsync()
    {
        throw new NotImplementedException();
    }

    public void Update(AppUser user)
    {
        throw new NotImplementedException();
    }
}
