using FalloutTerminalGame.Resources.Utils;
using FTG.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FalloutTerminalGame;

public partial class LoginPage(IUserService userService) : ContentPage
{
    public LoginPage() : this(ServiceHelper.GetService<IUserService>())
    {
        InitializeComponent();
    }
    private async void btnSubmit_Clicked(object sender, EventArgs e)
    {
        await AudioHelper.PlayAudioAsync(Constants.SelectionSound);
        if (txtUsername.Text.IsNullOrEmpty() || txtPassword.Text.IsNullOrEmpty() || !await userService.Login(txtUsername.Text, txtPassword.Text))
        {
            lblError.Text = "Incorrect credentials!";
        }
        else
        {
            await SecureStorage.SetAsync("username", txtUsername.Text);
            await Navigation.PopAsync();
        }
    }

    private async void btnRegister_Clicked(object sender, EventArgs e)
    {
        await AudioHelper.PlayAudioAsync(Constants.SelectionSound);
        if (txtUsername.Text.IsNullOrEmpty() || txtPassword.Text.IsNullOrEmpty() || !await userService.Register(txtUsername.Text, txtPassword.Text))
        {
            lblError.Text = "Incorrect credentials!";
        }
        else
        {
            await SecureStorage.SetAsync("username", txtUsername.Text);
            await Navigation.PopAsync();
        }
    }
}