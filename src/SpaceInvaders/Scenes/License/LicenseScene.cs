using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.License;

internal class LicenseScene : Scene
{
    private readonly License license = new();
    private readonly ScreenFade fade = new();

    public static Sprite? FontSprite { get; private set; }

    public LicenseScene()
    {
        Children.Add(license);
        Children.Add(fade);
    }

    public override void Init()
    {
        FontSprite = new(App.App.Window.RendererPtr, $"{AppInfo.TextureDire}Font16x16.png");

        base.Init();
    }

    public override void Finish()
    {
        FontSprite?.Dispose();

        base.Finish();
    }
}