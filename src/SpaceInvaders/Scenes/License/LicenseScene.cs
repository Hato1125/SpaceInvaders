using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.License;

internal class LicenseScene : Scene
{
    private static readonly LicenseProgress progress = new();

    public static bool IsStartProgress
    {
        get => progress.IsStartProgress;
        set => progress.IsStartProgress = value;
    }

    public LicenseScene()
    {
        Elements.Add(progress);
        Elements.Add(new LicenseText());
        Elements.Add(new ScreenFade());
    }
}