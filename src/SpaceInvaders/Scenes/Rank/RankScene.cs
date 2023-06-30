using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Rank;

internal class RankScene : Scene
{
    public static bool IsStartFadeOut;

    public static readonly RankList RankList = new();
    public static readonly RankInput RankInput = new();
    public static readonly ScreenFadeOut FadeOut = new();

    public RankScene()
    {
        Children.Add(RankInput);
        Children.Add(RankList);
        Children.Add(FadeOut);
    }
}