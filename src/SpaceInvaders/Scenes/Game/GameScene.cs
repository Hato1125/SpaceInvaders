using SpaceInvaders.Frame;
using SpaceInvaders.Scenes.Game.Gui;

namespace SpaceInvaders.Scenes.Game;

internal class GameScene : Scene
{
    public static readonly EnemyController Enemy = new();
    public static readonly PlayerController Player = new();
    public static readonly BeamScreen BeamScreen = new();
    public static readonly GameClear GameClear = new();
    public static readonly GuiScreen GuiScreen = new();

    public GameScene()
    {
        Children.Add(Enemy);
        Children.Add(Player);
        Children.Add(BeamScreen);
        Children.Add(GuiScreen);
        Children.Add(GameClear);
    }

    public override void Init()
    {
        GameInfo.Init();

        base.Init();
    }
}