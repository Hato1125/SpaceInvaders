using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Rank;

internal class RankScene : Scene
{
    public static readonly RankList RankList = new();
    public static readonly RankInput RankInput = new();

    public RankScene()
    {
        Children.Add(RankInput);
        Children.Add(RankList);
    }
}