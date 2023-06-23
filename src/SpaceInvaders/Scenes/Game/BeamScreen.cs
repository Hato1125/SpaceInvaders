using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Game;

internal class BeamScreen : Scene
{
    public static PlayerBeam? PlayerBeam;
    public static HashSet<EnemyBeam> EnemyBeam = new();

    public override void Update()
    {
        PlayerBeam?.Update();

        foreach(var beam in EnemyBeam)
            beam.Update();
    }

    public override void Render()
    {
        PlayerBeam?.Render();

        foreach(var beam in EnemyBeam)
            beam.Render();
    }

    public override void Finish()
    {
        PlayerBeam = null;
        EnemyBeam.Clear();
    }
}