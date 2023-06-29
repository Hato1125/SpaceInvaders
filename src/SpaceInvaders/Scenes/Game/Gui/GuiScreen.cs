using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Game.Gui;

internal class GuiScreen : Scene
{
    public GuiScreen()
    {
        Children.Add(new Header());
    }
}