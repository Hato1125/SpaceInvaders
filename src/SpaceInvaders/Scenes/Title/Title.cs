using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Resource;

namespace SpaceInvaders.Scenes.Title;

internal class Title : SceneElement
{
    public override void Render()
    {
        var titleSprite = SpriteManager.GetResource("TitleSprite");
        titleSprite.Render((AppInfo.Width - titleSprite.Width) / 2, 100);

        var charaSprite = SpriteManager.GetResource("CharaPointSprite");
        charaSprite.Render((AppInfo.Width - charaSprite.Width) / 2, 425);
    }
}