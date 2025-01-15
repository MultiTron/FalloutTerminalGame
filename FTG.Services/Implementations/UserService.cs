using FTG.Data;
using FTG.Data.Models;
using FTG.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FTG.Services.Implementations;
public class UserService(GameDbContext _context) : IUserService
{
    public async Task<int?> GetBestScoreByUsername(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null)
            return null;
        return user.BestScore;
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<List<User>> GetTop10()
    {
        return await _context.Users.OrderByDescending(x => x.BestScore).Take(10).ToListAsync();
    }

    public async Task<bool> Login(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        if (user != null && user.PasswordHash == HashPass(password))
        {
            return true;
        }

        return false;
    }

    public async Task<bool> Register(string username, string password)
    {
        if (await _context.Users.AnyAsync(u => u.Username.Equals(username)))
        {
            return false;
        }

        var user = new User
        {
            Username = username,
            PasswordHash = HashPass(password),
            BestScore = 0,
            CurrentScore = 0,
            CreatedOn = DateTime.UtcNow,
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task UpdateBestScore(string username, int score)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null)
            return;
        if (user.BestScore >= score)
            return;
        user.BestScore = score;
        await _context.SaveChangesAsync();
    }

    private string HashPass(string pass)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(pass);

            byte[] hashBytes = sha256.ComputeHash(bytes);

            StringBuilder hashStringBuilder = new StringBuilder();
            foreach (var byteValue in hashBytes)
            {
                hashStringBuilder.Append(byteValue.ToString("x2"));
            }

            return hashStringBuilder.ToString();
        }
    }
}
