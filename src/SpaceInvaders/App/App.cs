namespace SpaceInvaders.App;

internal static class App
{
    private static double nowTime;
    private static double lastTime;

    public static nint WindowPtr { get; private set; }
    public static nint RendererPtr { get; private set; }

    public static bool IsRunning { get; set; }
    public static double DeltaTime { get; private set; }

    public static void Running()
    {
        Setup();
        Loop();
        Close();
    }

    private static void Setup()
    {
        InitSDLComponent();

        IsRunning = true;
    }

    private static void Loop()
    {
        while(IsRunning)
        {
            while(SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
            {
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        IsRunning = false;
                        break;
                }
            }

            SDL.SDL_RenderClear(RendererPtr);
            SDL.SDL_SetRenderDrawColor(RendererPtr, 100, 100, 100, 255);
            SDL.SDL_RenderPresent(RendererPtr);
            LimmitFramelate();

            nowTime = SDL.SDL_GetPerformanceCounter();
            DeltaTime = (nowTime - lastTime) / SDL.SDL_GetPerformanceFrequency();
            lastTime = nowTime;

            Console.WriteLine(DeltaTime);
        }
    }

    private static void Close()
    {
        FinishSDLComponent();
    }

    private static void InitSDLComponent()
    {
        var winFlags = SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN;

        WindowPtr = SDL.SDL_CreateWindow(
            string.Empty,
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            AppInfo.Width,
            AppInfo.Height,
            winFlags
        );

        if (WindowPtr == nint.Zero)
        {
            throw new Exception("Failed to create Window.");
        }

        var renFlags = SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE
            | SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED
            | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC;

        RendererPtr = SDL.SDL_CreateRenderer(WindowPtr, -1, renFlags);

        if (RendererPtr == nint.Zero)
        {
            FinishSDLComponent(new SDLComponent[] {
                SDLComponent.Window
            });

            throw new Exception("Failed to create Renderer.");
        }

        if(SDL.SDL_Init(SDL.SDL_INIT_SENSOR) < 0)
        {
            FinishSDLComponent(new SDLComponent[] {
                SDLComponent.Window,
                SDLComponent.Renderer,
            });

            throw new Exception("Failed to Initialize SDL.");
        }

        if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) < 0)
        {
            FinishSDLComponent(new SDLComponent[] {
                SDLComponent.Window,
                SDLComponent.Renderer,
                SDLComponent.SDL
            });

            throw new Exception("Failed to Initialize Image.");
        }
    }

    private static void FinishSDLComponent(SDLComponent[]? components = null)
    {
        if(components == null)
        {
            SDL.SDL_DestroyWindow(WindowPtr);
            SDL.SDL_DestroyRenderer(RendererPtr);
            SDL.SDL_Quit();
            SDL_image.IMG_Quit();
        }
        else
        {
            foreach(var component in components)
            {
                switch(component)
                {
                    case SDLComponent.Window:
                        SDL.SDL_DestroyWindow(WindowPtr);
                        break;

                    case SDLComponent.Renderer:
                        SDL.SDL_DestroyRenderer(RendererPtr);
                        break;

                    case SDLComponent.SDL:
                        SDL.SDL_Quit();
                        break;

                    case SDLComponent.Image:
                        SDL_image.IMG_Quit();
                        break;
                }
            }
        }
    }

    private static void LimmitFramelate()
    {
        double ms = 1 / AppInfo.MaxFramelate;

        if(DeltaTime < ms)
        {
            double sleepMs = (ms - DeltaTime) * 1000;
            SDL.SDL_Delay((uint)sleepMs);
        }
    }

    private enum SDLComponent
    {
        Window,
        Renderer,
        SDL,
        Image,
    }
}