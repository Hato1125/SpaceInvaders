using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game.Gui;

internal class ScoreLabel
{
    private Font16x16? scoreLabel;

    public void Init()
    {
        if (Share.FontSprite != null)
        {
            scoreLabel = new(Share.FontSprite)
            {
                Scale = 2.25f,
                TextSpace = -5,
                LineSpace = 15,
                Text = "score",
            };
        }
    }

    public void Update()
    {
        if (scoreLabel != null)
            scoreLabel.Text = $"score\n{GameInfo.Score}";
    }

    public void Render()
    {
        scoreLabel?.Render(50, 30, FontArrangement.Center);
    }
}