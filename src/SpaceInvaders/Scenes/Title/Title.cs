using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Graphics;
using SpaceInvaders.Scenes.Round;

namespace SpaceInvaders.Scenes.Title;

internal class Title : Scene
{
    private Sprite? titleSprite;
    private Sprite? charaSprite;

    public override void Init()
    {
        RoundScene.RoundCount = 1;
        titleSprite = new(App.App.Window.RendererPtr, $"{AppInfo.TitleTextureDire}Title.png");
        charaSprite = new(App.App.Window.RendererPtr, $"{AppInfo.TitleTextureDire}CharaPoint.png");
    }

    public override void Render()
    {
        titleSprite?.Render((AppInfo.Width - titleSprite.Width) / 2, 100);
        charaSprite?.Render((AppInfo.Width - charaSprite.Width) / 2, 425);
    }

    public override void Finish()
    {
        titleSprite?.Dispose();
        charaSprite?.Dispose();
    }
}