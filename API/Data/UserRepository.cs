using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserRepository(DataContext _context, IMapper mapper)
    {
        this._context = _context;
        _mapper = mapper;
    }

    public async Task<MemberDTO> GetMemberAsync(string username)
    {
        // return await _context.Users  // without mapper
        // .Where(x => x.UserName == username).Select(user => new MemberDTO{
        //     Id = user.Id,
        //     UserName = user.UserName,
        //     KnownAs = user.KnownAs
        // }).SingleOrDefaultAsync(); 

        return await _context.Users.Where(x => x.UserName == username).ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
    {
        
        return await _context.Users.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.Include( p => p.Photos).SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _context.Users.Include( p => p.Photos).ToListAsync(); // eager loading photos by using .Include()
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0; // bool results
    }

    public void Update(AppUser user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }
}
