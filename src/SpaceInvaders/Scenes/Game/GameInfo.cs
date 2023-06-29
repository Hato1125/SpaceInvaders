namespace SpaceInvaders.Scenes.Game;

internal static class GameInfo
{
    public static int Score { get; set; }

    public static void Init()
    {
        Score = 0;
    }
}