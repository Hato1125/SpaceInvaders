using SpaceInvaders.App;
using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.License;

internal class ScreenFade : SceneElement
{
    private const float FADE_SPEED = 20;
    private const float DELAY_MS = 2.0f;
    public const float FADE_DELAY_MS = 5.0f;

    private SDL.SDL_Rect rect;
    private double fadeOpacity;
    private double fadeInCounter;
    private double fadeOutCounter;
    private double fadeDelayCounter;
    private double delayCounter;

    public ScreenFade()
    {
        rect.w = AppInfo.Width;
        rect.h = AppInfo.Height;
    }

    public override void Init()
    {
        fadeOpacity = 0;
        fadeInCounter = 0;
        fadeDelayCounter = 0;
        fadeOutCounter = 0;
        delayCounter = 0;
    }

    public override void Update()
    {
        fadeInCounter += FADE_SPEED * App.App.Window.DeltaTime;
        if(fadeInCounter > 90)
        {
            LicenseScene.IsStartProgress = true;
            fadeInCounter = 90;
            fadeDelayCounter += App.App.Window.DeltaTime;
            if (fadeDelayCounter > FADE_DELAY_MS)
            {
                fadeDelayCounter = FADE_DELAY_MS;
                fadeOutCounter += FADE_SPEED * App.App.Window.DeltaTime;
                if (fadeOutCounter > 90)
                    fadeOutCounter = 90;
            }
        }

        if(fadeOutCounter >= 90)
        {
            delayCounter += App.App.Window.DeltaTime;
            if (delayCounter > DELAY_MS)
            {
                delayCounter = DELAY_MS;
                SceneManager.ChangeScene("Title");
            }
        }

        fadeOpacity = Math.Sin((fadeInCounter - fadeOutCounter) * Math.PI / 180) * 255;
    }

    public override void Render()
    {
        var opacity = (byte)(255 - (byte)fadeOpacity);

        SDL.SDL_SetRenderDrawBlendMode(App.App.Window.RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
        SDL.SDL_SetRenderDrawColor(App.App.Window.RendererPtr, 0, 0, 0, opacity);
        SDL.SDL_RenderFillRect(App.App.Window.RendererPtr, ref rect);
        SDL.SDL_SetRenderDrawColor(App.App.Window.RendererPtr, 0, 0, 0, 0);
    }
}