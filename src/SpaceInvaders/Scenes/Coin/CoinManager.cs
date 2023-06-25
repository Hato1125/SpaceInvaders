namespace SpaceInvaders.Scenes;

internal static class CoinManager
{
    public static int Coin { get; private set; }

    public static void IncreCoin()
        => Coin++;

    public static void DecreCoin()
        => Coin--;
}