using System.Diagnostics;

namespace SpaceInvaders.App;

internal class WindowInfo
{
    public int Width { get; set; } = 0;
    public int Height { get; set; } = 0;
    public string WindowTitle { get; set; } = string.Empty;
    public SDL.SDL_WindowFlags WindowFlags { get; set; }
    public SDL.SDL_RendererFlags RendererFlags { get; set; }
}

internal class Window
{
    private readonly Stopwatch deltaWatch = new();
    private readonly WindowInfo windowInfo;

    public nint WindowPtr { get; private set; }
    public nint RendererPtr { get; private set; }
    public double MaxFramerate { get; set; }
    public double DeltaTime { get; private set; }

    public event Action? OnSetuping = delegate { };
    public event Action? OnSetuped = delegate { };
    public event Action? OnClosing = delegate { };
    public event Action? OnClosed = delegate { };
    public event Action? OnLoop = delegate { };
    public event Action<SDL.SDL_Event> OnEvent = delegate { };

    public bool IsRunning { get; private set; }

    public Window(
        string windowTitle,
        int windowWidth,
        int windowHeight,
        SDL.SDL_WindowFlags winFlags,
        SDL.SDL_RendererFlags renflags)
    {
        windowInfo = new()
        {
            WindowTitle = windowTitle,
            Width = windowWidth,
            Height = windowHeight,
            WindowFlags = winFlags,
            RendererFlags = renflags,
        };
    }

    public void Setup()
    {
        OnSetuping?.Invoke();
        CreateWindow();
        CreateRenderer();
        InitSDLComponent();
        OnSetuped?.Invoke();

        IsRunning = true;
    }

    public void Loop()
    {
        while (IsRunning)
        {
            DeltaTime = deltaWatch.Elapsed.TotalSeconds;
            deltaWatch.Restart();

            while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
            {
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        IsRunning = false;
                        break;
                }

                OnEvent?.Invoke(e);
            }

            SDL.SDL_RenderClear(RendererPtr);
            OnLoop?.Invoke();
            SDL.SDL_RenderPresent(RendererPtr);

            FramerateLimitter();
        }
    }

    public void Close()
    {
        OnClosing?.Invoke();
        SDL.SDL_DestroyWindow(WindowPtr);
        SDL.SDL_DestroyRenderer(RendererPtr);
        OnClosed?.Invoke();
    }

    private void InitSDLComponent()
    {
        if (SDL.SDL_Init(SDL.SDL_INIT_SENSOR | SDL.SDL_INIT_GAMECONTROLLER) < 0)
        {
            SDL.SDL_DestroyWindow(WindowPtr);
            SDL.SDL_DestroyRenderer(RendererPtr);

            throw new Exception("Failed to initialize SDL.");
        }

        if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) < 0)
        {
            SDL.SDL_DestroyWindow(WindowPtr);
            SDL.SDL_DestroyRenderer(RendererPtr);
            SDL.SDL_Quit();

            throw new Exception("Failed to initialize Image.");
        }
    }

    private void CreateWindow()
    {
        WindowPtr = SDL.SDL_CreateWindow(
            windowInfo.WindowTitle,
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            windowInfo.Width,
            windowInfo.Height,
            windowInfo.WindowFlags
        );

        if (WindowPtr == nint.Zero)
            throw new Exception("Failed to create Window.");
    }

    private void CreateRenderer()
    {
        RendererPtr = SDL.SDL_CreateRenderer(WindowPtr, -1, windowInfo.RendererFlags);

        if (RendererPtr == nint.Zero)
        {
            SDL.SDL_DestroyWindow(WindowPtr);
            throw new Exception("Failed to create Renderer.");
        }
    }

    private void FramerateLimitter()
    {
        if (MaxFramerate <= 0)
            return;

        double ms = 1.0 / MaxFramerate;

        if (deltaWatch.Elapsed.TotalSeconds < ms)
        {
            double sleepMs = (ms - deltaWatch.Elapsed.TotalSeconds) * 1000.0;
            SDL.SDL_Delay((uint)sleepMs);
        }
    }
}