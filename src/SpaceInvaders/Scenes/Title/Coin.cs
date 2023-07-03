using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Title;

internal class Coin : Scene
{
    public Coin()
    {
        Elements.Add(new CoinInput());
        Elements.Add(new CoinLabel());
    }
}