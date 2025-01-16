using Plugin.Maui.Audio;

namespace FalloutTerminalGame.Resources.Utils;
public static class AudioHelper
{
    public static async Task PlayAudioAsync(string filename)
    {
        var audioPlayer = AudioManager.Current.CreateAsyncPlayer(await FileSystem.OpenAppPackageFileAsync(filename));

        await audioPlayer.PlayAsync(CancellationToken.None);
    }
}
