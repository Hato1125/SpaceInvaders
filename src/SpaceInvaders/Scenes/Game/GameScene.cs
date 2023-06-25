using SpaceInvaders.Frame;
using SpaceInvaders.Scenes.Round;

namespace SpaceInvaders.Scenes.Game;

internal class GameScene : Scene
{
    public const int MAX_ROUND = 3;
    private const int DELAY_MS = 1;
    public static bool IsClear { get; set; }

    public static readonly EnemyController Enemy = new();
    public static readonly PlayerController Player = new();
    public static readonly BeamScreen BeamScreen = new();

    private double delayCounter;

    public GameScene()
    {
        Children.Add(Enemy);
        Children.Add(Player);
        Children.Add(BeamScreen);
    }

    public override void Init()
    {
        IsClear = false;
        delayCounter = 0;

        base.Init();
    }

    public override void Update()
    {
        if(IsClear)
        {
            delayCounter += App.App.Window.DeltaTime;
            if(delayCounter > DELAY_MS)
            {
                RoundScene.RoundCount++;
                if(RoundScene.RoundCount > MAX_ROUND)
                    SceneManager.ChangeScene("Title");
                else
                    SceneManager.ChangeScene("Round");
            }
            else
            {
                return;
            }
        }

        base.Update();
    }
}