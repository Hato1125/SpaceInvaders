using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.License;

internal class LicenseText : Scene
{
    private Sprite? fontSprite;
    private Font16x16? noticeFont;
    private Font16x16[]? licenseFont;

    private readonly string[] texts = new string[]
    {
        "This application is MIT licensed.",
        "The developer does not take any",
        "responsibility regarding this",
        "application. Also, we do not",
        "guarantee that the application",
        "will always work.",
    };

    private readonly string noticeText = "notice";

    public override void Init()
    {

        fontSprite = new(App.App.Window.RendererPtr, $"{AppInfo.TextureDire}Font16x16.png");

        licenseFont = new Font16x16[texts.Length];
        for(int i = 0; i < licenseFont.Length; i++)
        {
            licenseFont[i] = new(fontSprite)
            {
                Text = texts[i],
                Scale = 2.0f,
                TextSpace = -10,
                TextColor = Color.White,
            };
        }

        noticeFont = new(fontSprite)
        {
            Text = noticeText,
            Scale = 2.5f,
            TextSpace = -10,
            TextColor = Color.Red,
        };
    }

    public override void Render()
    {
        noticeFont?.Render((AppInfo.Width - noticeFont.Width) / 2, 170);

        if (licenseFont != null)
        {
            for (int i = 0; i < licenseFont.Length; i++)
            {
                licenseFont[i].Render(
                    (AppInfo.Width - licenseFont[i].Width) / 2,
                    (AppInfo.Height - licenseFont[i].Height * licenseFont.Length) / 2 + i * (licenseFont[i].Height + 3)
                );
            }
        }
    }

    public override void Finish()
    {
        fontSprite?.Dispose();
    }
}