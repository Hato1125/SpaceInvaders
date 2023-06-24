using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Title;

internal class Coin : Scene
{
    private readonly CoinInput input;
    private readonly CoinLabel label;

    private Sprite? fontSprite;

    public Coin()
    {
        input = new();
        label = new();
    }

    public override void Init()
    {
        fontSprite = new(App.App.Window.RendererPtr, $"{AppInfo.TextureDire}Font16x16.png");

        label.Init(fontSprite);
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

    public override void Finish()
    {
        fontSprite?.Dispose();
    }
}