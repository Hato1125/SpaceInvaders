using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Game;

internal class GameScene : Scene
{
    public GameScene()
    {
        Children.Add(new EnemyCell());
    }
}