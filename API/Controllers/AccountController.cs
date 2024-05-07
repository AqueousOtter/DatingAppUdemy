using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService){
        _context = context;
        _tokenService = tokenService;
    }


    [HttpPost("register")] // POST: /api/account/register?username=dave&password=pwd
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {
        if (await UserExists(registerDTO.Username)) return BadRequest("User Already Exists");
        using var hmac = new HMACSHA512(); // used using bc it will dispose automatically 

        var user = new AppUser
        {
            UserName = registerDTO.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDTO{
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };


    }


    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO) 
    {
        var user  = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginDTO.Username);

        if (user ==null) return Unauthorized("Invalid user");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

        for (int i = 0; i< computedHash.Length; i++){
            if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid PAsswordd");
        }


        return new UserDTO{
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };

    }

    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(user => user.UserName == username.ToLower());
    }


}
