using Microsoft.IdentityModel.Tokens;

namespace FalloutTerminalGame;

public partial class WellcomePage : ContentPage
{
    public WellcomePage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        var username = await SecureStorage.GetAsync("username");
        if (!username.IsNullOrEmpty())
        {
            lblWelcome.Text = $"Welcome {username}\nto the Fallout Terminal Game";
        }
        base.OnNavigatedTo(args);
    }

    private void btnPlay_Clicked(object sender, EventArgs e)
    {

    }

    private async void btnScoreboard_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ScorePage());
    }

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}