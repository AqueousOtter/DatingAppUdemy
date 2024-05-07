using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(DataContext context) // static so we dont need to create instance of class when we want to use the method. No New Seed() needed
    {
        if( await context.Users.AnyAsync()) return;

        // if no users
        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

        var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true}; /// helps incase caps are diff

        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);


        foreach (var user in users){
            using var hmac = new HMACSHA512();
            user.UserName = user.UserName.ToLower();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            user.PasswordSalt = hmac.Key;

            context.Users.Add(user); // adding it to EF tracking to add into the DB later
        }

        await context.SaveChangesAsync(); // adds to DB
    }

}
