using SpaceInvaders.Input;
using SpaceInvaders.Logger;

namespace SpaceInvaders.App;

internal static class App
{
    private static readonly SDL.SDL_WindowFlags windowFlags = SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN;
    private static readonly SDL.SDL_RendererFlags rendererFlags = SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED
        | SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE
        | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC;

    public static readonly Window Window = new(
        AppInfo.AppName,
        AppInfo.Width,
        AppInfo.Height,
        windowFlags,
        rendererFlags
    );

    public static void Running()
    {
        if (Window.IsRunning)
            return;

        Window.OnSetuped += Init;
        Window.OnLoop += Loop;
        Window.OnEvent += Event;
        Window.OnClosing += End;

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

        throw new Exception("test");
    }

    private static void Event(SDL.SDL_Event e)
    {
        GameController.UpdateEvent(e);
    }

    private static void Loop()
    {
        Keyboard.Update();
        GameController.Update();
    }

    private static void End()
    {
        Log.WriteInfo("[START] Discard game data.");
    }
}