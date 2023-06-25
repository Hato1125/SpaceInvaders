using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Round;

internal class RoundScene : Scene
{
    private const float DELAY_MS = 4;

    private Sprite? fontSprite;
    private Font16x16? roundLabel;
    private double delayCounter;

    public static int RoundCount { get; set; }

    public override void Init()
    {
        delayCounter = 0;

        fontSprite = new(App.App.Window.RendererPtr, $"{AppInfo.TextureDire}Font16x16.png");
        roundLabel = new(fontSprite)
        {
            Text = "round",
            Scale = 3.0f,
            TextSpace = -5,
        };
    }

    public override void Update()
    {
        delayCounter += App.App.Window.DeltaTime;
        if(delayCounter >= DELAY_MS)
        {
            delayCounter = DELAY_MS;
            SceneManager.ChangeScene("Game");
        }

        if (roundLabel != null)
            roundLabel.Text = $"round {RoundCount}";
    }

    public override void Render()
    {
        roundLabel?.Render(
            (AppInfo.Width - roundLabel.Width) / 2,
            (AppInfo.Height - roundLabel.Height) / 2
        );
    }

    public override void Finish()
    {
        fontSprite?.Dispose();
    }
}