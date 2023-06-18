namespace SpaceInvaders.App;

internal static class AppInfo
{
    public const int Width = 864;
    public const int Height = 756;

    public static readonly string AppName = "SpaceInvaders";
    public static readonly string LogFileName = $"{AppContext.BaseDirectory}Log\\app.log";
    public static readonly string ResourceDire = $"{AppContext.BaseDirectory}Resource\\";
}