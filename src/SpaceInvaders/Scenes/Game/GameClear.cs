using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Scenes.Round;

namespace SpaceInvaders.Scenes.Game;

internal class GameClear : Scene
{
    private readonly Scene[] waitScenes = new Scene[]
    {
        GameScene.BeamScreen,
        GameScene.Player,
    };

    private bool isGameClear;
    public bool IsGameClear
    {
        get => isGameClear;
        set
        {
            isGameClear = value;

            if (value)
                SceneManager.WaitScene(waitScenes, 2);
        }
    }

    private SDL.SDL_Rect fadeRect;
    private double fadeOutCounter;
    private double fadeOutOpacity;

    public GameClear()
    {
        fadeRect.w = AppInfo.Width;
        fadeRect.h = AppInfo.Height;
    }

    public override void Init()
    {
        IsGameClear = false;
        fadeOutCounter = 0;
        fadeOutOpacity = 0;
    }

    public override void Update()
    {
        if (!isGameClear)
            return;

        fadeOutCounter += 90 / 2 * App.App.Window.DeltaTime;
        if (fadeOutCounter > 90)
        {
            fadeOutCounter = 90;
            fadeOutOpacity = 255;

            TransitionToRoundOrTitle();
        }
        else
        {
            fadeOutOpacity = Math.Sin(fadeOutCounter * Math.PI / 180) * 255;
        }
    }

    public override void Render()
    {
        if (!isGameClear)
            return;

        SDL.SDL_SetRenderDrawBlendMode(App.App.Window.RendererPtr, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
        SDL.SDL_SetRenderDrawColor(App.App.Window.RendererPtr, 0, 0, 0, (byte)fadeOutOpacity);
        SDL.SDL_RenderFillRect(App.App.Window.RendererPtr, ref fadeRect);
        SDL.SDL_SetRenderDrawColor(App.App.Window.RendererPtr, 0, 0, 0, 0);
    }

    private void TransitionToRoundOrTitle()
    {
        RoundScene.RoundCount++;
        if (RoundScene.RoundCount > 3)
            SceneManager.ChangeScene("Title");
        else
            SceneManager.ChangeScene("Round");
    }
}