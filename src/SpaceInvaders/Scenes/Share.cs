using SpaceInvaders.App;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes;

internal static class Share
{
    public static Sprite? FontSprite { get; private set; }

    public static void Init()
    {
        FontSprite = new(App.App.Window.RendererPtr, $"{AppInfo.TextureDire}Font16x16.png");
    }

    public static void Finish()
    {
        FontSprite?.Dispose();
    }
}