using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Game;

internal class GameScene : Scene
{
    public static readonly EnemyController Enemy = new();
    public static readonly PlayerController Player = new();
    public static readonly BeamScreen BeamScreen = new();
    public static readonly GameClear GameClear = new();

    public GameScene()
    {
        Children.Add(GameClear);
        Children.Add(Enemy);
        Children.Add(Player);
        Children.Add(BeamScreen);
    }
}