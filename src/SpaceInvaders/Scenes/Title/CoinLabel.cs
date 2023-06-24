using SpaceInvaders.App;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Title;

internal class CoinLabel
{
    private readonly Color[] flashColors = new Color[]
    {
        Color.White,
        Color.Yellow,
    };

    private readonly string[] promptTexts = new string[]
    {
        "Insert coin to start",
        "Press button to start",
    };

    private readonly string buttonText = "Press one of the A B X Y buttons";

    private Sprite? fontSprite;
    private Font16x16? promptFont;
    private Font16x16? buttonFont;
    private Font16x16? coinFont;

    private int flashIndex;
    private double flashCounter;

    public void Init(Sprite font)
    {
        fontSprite = font;

        promptFont = new(fontSprite)
        {
            Text = promptTexts[0],
            TextSpace = -10,
            TextColor = Color.White,
            Scale = 2.0f,
        };

        buttonFont = new(fontSprite)
        {
            Text = buttonText,
            TextSpace = -10,
            TextColor = Color.Yellow,
            Scale = 1.25f,
        };

        coinFont = new(fontSprite)
        {
            Text = string.Empty,
            TextSpace = -10,
            TextColor = Color.White,
            Scale = 1.25f,
        };
    }

    public void Update()
    {
        flashCounter += App.App.Window.DeltaTime;
        if (flashCounter > 1.0)
        {
            flashCounter = 0;

            if (flashIndex > 0)
                flashIndex = 0;
            else
                flashIndex++;
        }

        if(promptFont != null)
        {
            var textIndex = CoinManager.Coin > 0 ? 1 : 0;

            promptFont.Text = promptTexts[textIndex];
            promptFont.TextColor = flashColors[flashIndex];
        }

        if(coinFont != null)
            coinFont.Text = $"Coin {CoinManager.Coin}";
    }

    public void Render()
    {
        promptFont?.Render((AppInfo.Width - promptFont.Width) / 2, 300);

        if(CoinManager.Coin > 0)
            buttonFont?.Render((AppInfo.Width - buttonFont.Width) / 2, 350);

        coinFont?.Render((AppInfo.Width - coinFont.Width) / 2, 720);
    }
}