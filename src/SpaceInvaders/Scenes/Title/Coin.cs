using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Title;

internal class Coin : Scene
{
    private readonly CoinInput input;
    private readonly CoinLabel label;

    public Coin()
    {
        input = new();
        label = new();
    }

    public override void Init()
    {
        label.Init();
    }

    public override void Update()
    {
        input.Update();
        label.Update();
    }

    public override void Render()
    {
        label.Render();
    }
}