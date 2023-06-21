using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Game;

internal class BeamScreen : Scene
{
    public static PlayerBeamComponent? PlayerBeam;

    public override void Update()
    {
        PlayerBeam?.Update();
    }

    public override void Render()
    {
        PlayerBeam?.Render();
    }

    public override void Finish()
    {
        PlayerBeam = null;
    }
}