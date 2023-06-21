using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Game;

internal class GameScene : Scene
{
    public GameScene()
    {
        Children.Add(new BeamScreen());
        Children.Add(new Player());
        Children.Add(new EnemyCell());
    }
}