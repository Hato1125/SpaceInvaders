using SpaceInvaders.App;
using SpaceInvaders.Resource;

namespace SpaceInvaders;

internal static class SL
{
    public static void LoadSprites()
    {
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "FontSprite", $"{AppInfo.TextureDire}Font16x16.png");
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "TitleSprite", $"{AppInfo.TitleTextureDire}Title.png");
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "CharaPointSprite", $"{AppInfo.TitleTextureDire}CharaPoint.png");
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "Enemy0_0", $"{AppInfo.GameTextureDire}Enemy_0\\0.png");
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "Enemy0_1", $"{AppInfo.GameTextureDire}Enemy_0\\1.png");
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "Enemy1_0", $"{AppInfo.GameTextureDire}Enemy_1\\0.png");
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "Enemy1_1", $"{AppInfo.GameTextureDire}Enemy_1\\1.png");
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "Enemy2_0", $"{AppInfo.GameTextureDire}Enemy_2\\0.png");
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "Enemy2_1", $"{AppInfo.GameTextureDire}Enemy_2\\1.png");
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "EnemyBeam", $"{AppInfo.GameTextureDire}EnemyBeam.png");
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "PlayerBeam", $"{AppInfo.GameTextureDire}PlayerBeam.png");
        SpriteManager.RegistSprite(App.App.Window.RendererPtr, "Player", $"{AppInfo.GameTextureDire}Player.png");
    }
}