namespace FalloutTerminalGame;

public partial class WellcomePage : ContentPage
{
    public WellcomePage()
    {
        InitializeComponent();
    }

    private void btnPlay_Clicked(object sender, EventArgs e)
    {

    }

    private void btnScoreboard_Clicked(object sender, EventArgs e)
    {

    }

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }

    private void btnRegister_Clicked(object sender, EventArgs e)
    {

    }
}