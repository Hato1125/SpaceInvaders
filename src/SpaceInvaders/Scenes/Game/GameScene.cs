using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Game;

internal class GameScene : Scene
{
    public static readonly EnemyController Enemy = new();
    public static readonly PlayerController Player = new();

    public GameScene()
    {
        Children.Add(Enemy);
        Children.Add(Player);
        Children.Add(new BeamScreen());
    }
}