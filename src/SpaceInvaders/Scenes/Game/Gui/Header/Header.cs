using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Game.Gui;

internal class Header : Scene
{
    private readonly ScoreLabel score = new();

    public override void Init()
    {
        score.Init();
    }

    public override void Update()
    {
        score.Update();
    }

    public override void Render()
    {
        score.Render();
    }
}