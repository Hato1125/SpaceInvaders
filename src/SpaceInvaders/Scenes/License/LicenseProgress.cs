using SpaceInvaders.App;
using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.License;

internal class LicenseProgress : SceneElement
{
    private const int PROGRESS_WIDTH = 500;

    private SDL.SDL_FRect progressRect;
    private double progressCounter;

    public bool IsStartProgress { get; set; }

    public override void Init()
    {
        IsStartProgress = false;
        progressRect.w = 0;
        progressCounter = 0;
    }

    public override void Update()
    {
        if (!IsStartProgress)
            return;

        progressCounter += PROGRESS_WIDTH / ScreenFade.FADE_DELAY_MS * App.App.Window.DeltaTime;
        if(progressCounter > PROGRESS_WIDTH)
            progressCounter = PROGRESS_WIDTH;
    }

    public override void Render()
    {
        progressRect.h = 30;
        progressRect.y = 625;
        progressRect.x = (AppInfo.Width - PROGRESS_WIDTH) / 2.0f;

        SDL.SDL_SetRenderDrawColor(App.App.Window.RendererPtr, 100, 0, 0, 255);
        progressRect.w = PROGRESS_WIDTH;
        SDL.SDL_RenderFillRectF(App.App.Window.RendererPtr, ref progressRect);
        SDL.SDL_SetRenderDrawColor(App.App.Window.RendererPtr, 255, 0, 0, 255);
        progressRect.w = (float)progressCounter;
        SDL.SDL_RenderFillRectF(App.App.Window.RendererPtr, ref progressRect);
        SDL.SDL_SetRenderDrawColor(App.App.Window.RendererPtr, 0, 0, 0, 255);
    }
}