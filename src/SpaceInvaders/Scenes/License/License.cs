using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.License;

internal class License : Scene
{
    private readonly LicenseText text = new();
    private static readonly LicenseProgress progress = new();

    public static bool IsStartProgress
    {
        get => progress.IsStartProgress;
        set => progress.IsStartProgress = value;
    }

    public override void Init()
    {
        text.Init();
        progress.Init();
    }

    public override void Update()
    {
        progress.Update();
    }

    public override void Render()
    {
        text.Render();
        progress.Render();
    }

    public override void Finish()
    {
    }
}