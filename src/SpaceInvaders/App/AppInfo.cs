namespace SpaceInvaders.App;

internal static class AppInfo
{
    public const int Width = 864;
    public const int Height = 756;
    public const int MaxFPS = 60;

    public static readonly string AppName = "SpaceInvaders";
    public static readonly string LogFileName = $"{AppContext.BaseDirectory}Log\\app.log";
    public static readonly string ScoreDatabaseName = $"{AppContext.BaseDirectory}Resource\\Database\\score.db";
    public static readonly string TextureDire = $"{AppContext.BaseDirectory}Resource\\Texture\\";
    public static readonly string TitleTextureDire = $"{TextureDire}Title\\";
    public static readonly string GameTextureDire = $"{TextureDire}Game\\";
    public static readonly string RankTextureDire = $"{TextureDire}Rank\\";
    public static readonly string SoundDire = $"{AppContext.BaseDirectory}Resource\\Sound\\";
}