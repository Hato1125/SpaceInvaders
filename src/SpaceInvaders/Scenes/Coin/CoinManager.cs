namespace SpaceInvaders.Scenes;

internal static class CoinManager
{
    public static int Coin { get; set; }

    public static void PushCoin()
        => Coin++;

    public static void DecreCoin()
        => Coin--;
}