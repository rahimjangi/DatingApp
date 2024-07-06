using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Data;

public class Seed
{
    public static async Task SeedData(DataContext context)
    {
        if (await context.Users.AnyAsync()) return;
        Console.WriteLine("001_");
        var data = await File.ReadAllTextAsync("Data/UserSeedData.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var users = JsonSerializer.Deserialize<List<AppUser>>(data, options);
        if (users == null) return;
        foreach (var user in users)
        {
            Console.WriteLine(user);
            using var hmac = new HMACSHA512();
            user.UserName = user.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
            user.PasswordSalt = hmac.Key;
            context.Users.Add(user);
        }
        await context.SaveChangesAsync();
    }
}
