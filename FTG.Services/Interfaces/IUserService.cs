using FTG.Data.Models;

namespace FTG.Services.Interfaces;
public interface IUserService
{
    public Task<bool> Login(string username, string password);
    public Task<bool> Register(string username, string password);
    public Task<int?> GetBestScoreByUsername(string username);
    public Task<List<User>> GetTop10();
}
