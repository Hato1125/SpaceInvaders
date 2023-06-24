using SpaceInvaders.App;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.License;

internal class LicenseText
{
    private readonly string licenseText = """
        This application is MIT licensed.
        The developer does not take any r
        esponsibility regarding this appl
        ication. Also, we do not guarante
        e that the application will alway
        s work.
        """;

    private readonly string noticeText = "notice";

    private Font16x16? noticeLabel;
    private Font16x16? licenseLabel;

    public void Init()
    {
        if (LicenseScene.FontSprite == null)
            return;

        noticeLabel = new(LicenseScene.FontSprite)
        {
            Scale = 2.75f,
            TextSpace = -10,
            Text = noticeText,
            TextColor = Color.Red,
        };

        licenseLabel = new(LicenseScene.FontSprite)
        {
            Scale = 2.0f,
            TextSpace = -10,
            Text = licenseText,
            TextColor = Color.White,
        };
    }

    public void Render()
    {
        noticeLabel?.Render((AppInfo.Width - noticeLabel.Width) / 2.0f, 150);
        licenseLabel?.Render((AppInfo.Width - licenseLabel.Width) / 2.0f, 250, FontArrangement.Center);
    }
}