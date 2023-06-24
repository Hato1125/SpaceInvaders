using SpaceInvaders.App;
using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.License;

internal class ScreenFade : Scene
{
    private SDL.SDL_Rect rect;
    private double fadeCounter;
    private double fadeOpacity;
    private double coolCounter;

    public ScreenFade()
    {
        rect.w = AppInfo.Width;
        rect.h = AppInfo.Height;
    }

    public override void Init()
    {
        fadeCounter = 0;
        fadeOpacity = 0;
        coolCounter = 0;
    }

    public override void Update()
    {
        fadeCounter += 25 * App.App.Window.DeltaTime;
        if (fadeCounter > 180)
        {
            fadeOpacity = 0;
            fadeCounter = 180;
            coolCounter += App.App.Window.DeltaTime;

            if (coolCounter > 2)
                SceneManager.ChangeScene("Title");
        }
        else
        {
            fadeOpacity = Math.Sin(fadeCounter * Math.PI / 180) * 255;
        }
    }

    public override void Render()
    {
        SDL.SDL_SetRenderDrawBlendMode(App.App.Window.RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
        SDL.SDL_SetRenderDrawColor(App.App.Window.RendererPtr, 0, 0, 0, (byte)(255 - (byte)fadeOpacity));
        SDL.SDL_RenderFillRect(App.App.Window.RendererPtr, ref rect);
        SDL.SDL_SetRenderDrawColor(App.App.Window.RendererPtr, 0, 0, 0, 0);
    }
}