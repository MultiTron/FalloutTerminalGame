using FalloutTerminalGame.Resources.Utils;
using FTG.Services.Interfaces;
using System.Text;

namespace FalloutTerminalGame
{
    public partial class MainPage(IUserService userService, IWordService wordService) : ContentPage
    {
        private int lives;
        private int currentScore;
        private string? correctWord;
        public MainPage() : this(ServiceHelper.GetService<IUserService>(), ServiceHelper.GetService<IWordService>())
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            lives = 3;
            currentScore = 0;
            txtInput.Text = "";
            lblLikeness.Text = "";
            NewGame();
            await AudioHelper.PlayAudioAsync(Constants.NewGameSound);
            base.OnAppearing();
        }

        protected override async void OnDisappearing()
        {
            await userService.UpdateBestScore(await SecureStorage.GetAsync(Constants.Username), currentScore);
            base.OnDisappearing();
        }

        private async void btnEnter_Clicked(object sender, EventArgs e)
        {

            await AudioHelper.PlayAudioAsync(Constants.SelectionSound);
            int likeness = WordLinkeness.Calculate(txtInput.Text, correctWord!);
            lblLikeness.Text = $"Likeness: {likeness}";
            if (likeness == correctWord.Length)
            {
                currentScore += 5000;
                lives += 3;
                await AudioHelper.PlayAudioAsync(Constants.PointsUpSound);
                RefreshDisplay();
                NewGame();
                return;
            }
            currentScore += likeness * 100;
            lives--;
            RefreshDisplay();
            await AudioHelper.PlayAudioAsync(Constants.WrongSound);
            if (lives == 0)
            {
                await userService.UpdateBestScore(await SecureStorage.GetAsync(Constants.Username), currentScore);
                await Navigation.PushAsync(new WellcomePage());
            }
        }

        private async void NewGame()
        {
            var words = await wordService.GetRandomWords(10);
            correctWord = words[Random.Shared.Next(0, words.Count)].Value;
            StringBuilder sb = new StringBuilder();
            foreach (var word in words)
            {
                sb.AppendLine($"-@+*=(/ {word.Value} $#^*(/-");
            }
            lblPassPanel.Text = sb.ToString();
            lblLikeness.Text = "";
            RefreshDisplay();
        }
        private void RefreshDisplay()
        {
            lblLives.Text = lives.ToString();
            lblScore.Text = currentScore.ToString();
        }
    }
}
