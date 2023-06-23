using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Game;

internal class GameScene : Scene
{
    public static readonly Player Player = new();

    public GameScene()
    {
        Children.Add(new EnemyController());
        Children.Add(Player);
        Children.Add(new BeamScreen());
    }
}