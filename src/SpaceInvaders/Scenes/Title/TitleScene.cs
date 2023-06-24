using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Title;

internal class TitleScene : Scene
{
    public TitleScene()
    {
        Children.Add(new Coin());
        Children.Add(new Title());
    }
}