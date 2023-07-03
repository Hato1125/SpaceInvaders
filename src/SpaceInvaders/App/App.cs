using SpaceInvaders.Frame;
using SpaceInvaders.Input;
using SpaceInvaders.Logger;
using SpaceInvaders.Database;
using SpaceInvaders.Resource;
using SpaceInvaders.Scenes.Game;
using SpaceInvaders.Scenes.Rank;
using SpaceInvaders.Scenes.Title;
using SpaceInvaders.Scenes.Round;
using SpaceInvaders.Scenes.License;

namespace SpaceInvaders.App;

internal static class App
{
    private static readonly SDL.SDL_WindowFlags windowFlags = SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN;
    private static readonly SDL.SDL_RendererFlags rendererFlags = SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED
        | SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE;

    public static readonly Window Window = new(
        AppInfo.AppName,
        AppInfo.Width,
        AppInfo.Height,
        windowFlags,
        rendererFlags
    );

    public static readonly Random Random = new(4545);

    public static void Running()
    {
        if (Window.IsRunning)
            return;

        Window.OnSetuped += Init;
        Window.OnLoop += Loop;
        Window.OnEvent += Event;
        Window.OnClosing += End;
        Window.MaxFramerate = AppInfo.MaxFPS;

        try
        {
            Log.Initializing(AppInfo.LogFileName);
            Window.Setup();
            Window.Loop();
            Window.Close();
        }
        catch (Exception e)
        {
            var exceptionPrompt = "[EXCEPTIOM] An unexpected exception has occurred.";
            var exceptionMessage = $"Message: {e.Message}";
            var exceptionTrace = $"StackTrace: {e.StackTrace}";

            Log.WriteFatal($"{exceptionPrompt}\n{exceptionMessage}\n{exceptionTrace}");

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }
        finally
        {
            Log.Finalizing();
        }
    }

    private static void Init()
    {
        Log.WriteInfo("[START] initializing game data.");

        ScoreDataManager.Initializing(AppInfo.ScoreDatabaseName);

        SL.LoadSprites();
        SceneManager.AddScene("Game", new GameScene());
        SceneManager.AddScene("Title", new TitleScene());
        SceneManager.AddScene("License", new LicenseScene());
        SceneManager.AddScene("Round", new RoundScene());
        SceneManager.AddScene("Rank", new RankScene());
        SceneManager.ChangeScene("License");
    }

    private static void Event(SDL.SDL_Event e)
    {
        GameController.UpdateEvent(e);
    }

    private static void Loop()
    {
        Keyboard.Update();
        GameController.Update();

#if DEBUG
        if (Keyboard.IsPushed(SDL.SDL_Scancode.SDL_SCANCODE_HOME))
            SceneManager.ChangeScene("License");

        if (Keyboard.IsPushed(SDL.SDL_Scancode.SDL_SCANCODE_G))
            GC.Collect();

        if(Keyboard.IsPushed(SDL.SDL_Scancode.SDL_SCANCODE_D))
        {
            var score = new ScoreData()
            {
                Name = $"TestName: {Random.Next(0, 1000)}",
                Score = Random.Next(0, 1000),
            };

            ScoreDataManager.AddScore(score);
        }

        if (Keyboard.IsPushed(SDL.SDL_Scancode.SDL_SCANCODE_DELETE))
            ScoreDataManager.AllDelete();
#endif

        SceneManager.SceneUpdate();
    }

    private static void End()
    {
        Log.WriteInfo("[START] Discard game data.");

        ScoreDataManager.Finalizing();
        SceneManager.RemoveAllScene();
        SpriteManager.DeleteAllResource();
    }
}