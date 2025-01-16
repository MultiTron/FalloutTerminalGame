using Plugin.Maui.Audio;

namespace FalloutTerminalGame.Resources.Utils;
public static class AudioHelper
{
    public static async Task PlayAudioAsync(string filename)
    {
        var audioPlayer = AudioManager.Current.CreateAsyncPlayer(await FileSystem.OpenAppPackageFileAsync(filename));

        await audioPlayer.PlayAsync(CancellationToken.None);
    }
    public static void PlayAudio(string filename)
    {
        var audioPlayer = AudioManager.Current.CreatePlayer(filename);

        audioPlayer.Play();
    }
}
