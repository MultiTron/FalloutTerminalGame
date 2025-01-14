namespace FalloutTerminalGame.Resources.Utils;
public static class WordLinkeness
{
    public static int Calculate(string guess, string correct)
    {
        var likeness = 0;
        for (int i = 0; i < guess.Length && i < correct.Length; i++)
        {
            if (guess[i] == correct[i])
            {
                likeness++;
            }
        }
        return likeness;
    }
}
