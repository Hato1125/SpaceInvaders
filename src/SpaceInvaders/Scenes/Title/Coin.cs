using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Title;

internal class Coin : Scene
{
    public Coin()
    {
        Elements.Add(new CoinInput(this));
        Elements.Add(new CoinLabel(this));
    }
}