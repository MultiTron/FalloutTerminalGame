namespace FTG.Data.Models;
public class User : Entity
{
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required int BestScore { get; set; }
    public required int CurrentScore { get; set; }
}
