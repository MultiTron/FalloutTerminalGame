using FTG.Data.Models;

namespace FTG.Services.Interfaces;
public interface IUserService
{
    public Task<User> Login(string username, string password);
    public Task Register(string username, string password);
}
