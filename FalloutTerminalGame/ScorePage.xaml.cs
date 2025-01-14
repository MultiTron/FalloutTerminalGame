using FalloutTerminalGame.Resources.Utils;
using FTG.Services.Interfaces;
using System.Text;

namespace FalloutTerminalGame;

public partial class ScorePage(IUserService userService) : ContentPage
{
    public ScorePage() : this(ServiceHelper.GetService<IUserService>())
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        var score = await userService.GetBestScoreByUsername(await SecureStorage.GetAsync("username") ?? "");
        if (score != null)
        {
            lblBestScore.Text = $"Your best score is: {score}";
        }
        StringBuilder str = new StringBuilder();
        int counter = 1;
        foreach (var user in await userService.GetTop10())
        {
            str.AppendLine($"{counter}. {user.Username} - {user.BestScore}");
            counter++;
        }
        lblScoreboard.Text = str.ToString();
        base.OnAppearing();
    }
}