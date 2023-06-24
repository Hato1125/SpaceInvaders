using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.License;

internal class LicenseScene : Scene
{
    public LicenseScene()
    {
        Children.Add(new LicenseText());
        Children.Add(new ScreenFade());
    }
}