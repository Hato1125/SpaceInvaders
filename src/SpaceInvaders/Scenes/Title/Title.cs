using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Graphics;
using SpaceInvaders.Scenes.Round;

namespace SpaceInvaders.Scenes.Title;

internal class Title : Scene
{
    private Sprite? titleSprite;

    public override void Init()
    {
        RoundScene.RoundCount = 1;
        titleSprite = new(App.App.Window.RendererPtr, $"{AppInfo.TitleTextureDire}Title.png");
    }

    public override void Render()
    {
        titleSprite?.Render((AppInfo.Width - titleSprite.Width) / 2, 100);
    }

    public override void Finish()
    {
        titleSprite?.Dispose();
    }
}