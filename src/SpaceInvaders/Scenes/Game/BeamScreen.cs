using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Game;

internal class BeamScreen : Scene
{
    private PlayerBeam? playerBeam;
    private readonly HashSet<EnemyBeam> enemyBeam = new();

    public override void Update()
    {
        playerBeam?.Update();

        foreach(var beam in enemyBeam)
            beam.Update();
    }

    public override void Render()
    {
        playerBeam?.Render();

        foreach(var beam in enemyBeam)
            beam.Render();
    }

    public override void Finish()
    {
        playerBeam = null;
        enemyBeam.Clear();
    }

    public void PushBeam(PlayerBeam beam)
        => playerBeam = beam;

    public void PushBeam(EnemyBeam beam)
        => enemyBeam.Add(beam);

    public void RemoveBeam()
        => playerBeam = null;

    public void RemoveBeam(EnemyBeam beam)
        => enemyBeam.Remove(beam);

    public bool AnyPlayerBeam()
        => playerBeam == null;
}