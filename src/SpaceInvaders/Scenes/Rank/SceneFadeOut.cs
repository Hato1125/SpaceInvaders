using SpaceInvaders.App;
using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Rank;

internal class ScreenFadeOut : Scene
{
    private const float FADE_SPEED = 50.0f;
    private const float DELAY_MS = 2.0f;

    private SDL.SDL_Rect rect;
    private double fadeCounter;
    private double fadeOpacity;
    private double delayCounter;

    public ScreenFadeOut()
    {
        rect.w = AppInfo.Width;
        rect.h = AppInfo.Height;
    }

    public override void Init()
    {
        fadeCounter = 0;
        fadeOpacity = 0;
        delayCounter = 0;
    }

    public override void Update()
    {
        if(!RankScene.IsStartFadeOut)
            return;

        fadeCounter += FADE_SPEED * App.App.Window.DeltaTime;
        if(fadeCounter > 90)
        {
            fadeCounter = 90;
            fadeOpacity = 255;

            delayCounter += App.App.Window.DeltaTime;
            if(delayCounter > DELAY_MS)
            {
                delayCounter = DELAY_MS;
                SceneManager.ChangeScene("Title");
            }
        }
        else
        {
            fadeOpacity = Math.Sin(fadeCounter * Math.PI / 180) * 255;
        }
    }

    public override void Render()
    {
        if(!RankScene.IsStartFadeOut)
            return;

        var opacity = (byte)fadeOpacity;

        SDL.SDL_SetRenderDrawBlendMode(App.App.Window.RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
        SDL.SDL_SetRenderDrawColor(App.App.Window.RendererPtr, 0, 0, 0, opacity);
        SDL.SDL_RenderFillRect(App.App.Window.RendererPtr, ref rect);
        SDL.SDL_SetRenderDrawColor(App.App.Window.RendererPtr, 0, 0, 0, 0);
    }
}