using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Graphics;
using SpaceInvaders.Resource;
using SpaceInvaders.Scenes.Round;

namespace SpaceInvaders.Scenes.Title;

internal class Title : Scene
{
    public Title()
    {
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "TitleSprite", $"{AppInfo.TitleTextureDire}Title.png");
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "CharaPointSprite", $"{AppInfo.TitleTextureDire}CharaPoint.png");
    }

    public override void Render()
    {
        var titleSprite = SpriteManager.GetResource("TitleSprite");
        titleSprite?.Render((AppInfo.Width - titleSprite.Width) / 2, 100);

        var charaSprite = SpriteManager.GetResource("CharaPointSprite");
        charaSprite?.Render((AppInfo.Width - charaSprite.Width) / 2, 425);
    }
}