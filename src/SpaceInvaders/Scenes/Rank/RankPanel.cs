using SpaceInvaders.Database;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Rank;

internal class RankPanel
{
    public readonly ScoreData scoreData;
    private readonly Sprite scorePanel;
    private readonly Font16x16 rankLabel;
    private readonly Font16x16 nameLabel;
    private readonly Font16x16 scoreLabel;

    public float X { get; set; }
    public float Y { get; set; }

    public RankPanel(ScoreData data, Sprite panel, Sprite font, int rank)
    {
        scoreData = data;
        scorePanel = panel;

        rankLabel = new(font)
        {
            Scale = 2.0f,
            TextSpace = -15,
            Text = $"{rank} {GetOrderStr(rank)}",
            TextColor = GetRankColor(rank),
        };

        nameLabel = new(font)
        {
            Scale = 2.0f,
            TextSpace = -15,
            Text = data.Name ?? "Null",
        };

        scoreLabel = new(font)
        {
            Scale = 1.25f,
            TextSpace = -5,
            Text = $"score {data.Score}",
        };
    }

    public void Render()
    {
        scorePanel?.Render(X, Y);
        rankLabel?.Render(X + 10, Y + 10);
        nameLabel?.Render(X + 150, Y + 10);
        scoreLabel?.Render(X + 10, Y + 55);
    }

    private static Color GetRankColor(int rank) => rank switch
    {
        1 => Color.Gold,
        2 => Color.Silver,
        3 => Color.Brown,
        _ => Color.White
    };

    private static string GetOrderStr(int rank) => rank switch
    {
        1 => "st",
        2 => "nd",
        3 => "rd",
        _ => "th"
    };
}