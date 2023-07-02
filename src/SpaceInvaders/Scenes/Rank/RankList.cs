using System.Runtime.InteropServices;
using SpaceInvaders.App;
using SpaceInvaders.Input;
using SpaceInvaders.Frame;
using SpaceInvaders.Database;
using SpaceInvaders.Graphics;
using SpaceInvaders.Resource;

namespace SpaceInvaders.Scenes.Rank;

internal class RankList : Scene
{
    private const float SCROLL_SPEED = 600;

    private List<RankPanel> panelList = new();
    private Sprite? panelSprite;

    private float scrollMax;
    private float scrollValue;

    public override void Init()
    {
        panelSprite = new(App.App.Window.RendererPtr, $"{AppInfo.RankTextureDire}Panel.png");

        var fontSprite = SpriteManager.GetResource("FontSprite");

        var data = ScoreDataManager.GetAllData();
        if (data != null)
        {
            var dataList = data.OrderByDescending(d => d.Score).ToList();
            var dataSpan = CollectionsMarshal.AsSpan(dataList);

            for (int i = 0; i < dataSpan.Length; i++)
            {
                var panel = new RankPanel(dataSpan[i], panelSprite, fontSprite, i + 1)
                {
                    X = (AppInfo.Width - panelSprite.Width) / 2,
                };
                panelList.Add(panel);
            }

            scrollMax = (panelSprite.Height) * panelList.Count - AppInfo.Height;
        }
    }

    public override void Update()
    {
        if (panelSprite == null)
            return;

        for (int i = 0; i < panelList.Count; i++)
            panelList[i].Y = panelSprite.Height * i - scrollValue;
    }

    public override void Render()
    {
        if (panelSprite == null)
            return;

        for (int i = 0; i < panelList.Count; i++)
            panelList[i].Render();
    }

    public void DownRankList()
    {
        if (scrollValue <= scrollMax)
        {
            scrollValue += (float)(SCROLL_SPEED * App.App.Window.DeltaTime);
        }
    }

    public void UpRankList()
    {
        if (scrollValue >= 0)
        {
            scrollValue -= (float)(SCROLL_SPEED * App.App.Window.DeltaTime);
        }
    }
}