namespace SpaceInvaders.Input;

internal static class GameController
{
    public const int GAMECONTROLLER_BUTTON_NUM = 23;

    private static readonly List<nint> registController = new();
    private static readonly Dictionary<nint, sbyte[]> gameControllers = new();

    public static void UpdateEvent(in SDL.SDL_Event e)
    {
        switch (e.type)
        {
            case SDL.SDL_EventType.SDL_JOYDEVICEADDED:
                var addController = SDL.SDL_GameControllerOpen(e.cdevice.which);

                registController.Add(addController);
                gameControllers.Add(addController, new sbyte[GAMECONTROLLER_BUTTON_NUM]);
                break;

            case SDL.SDL_EventType.SDL_JOYDEVICEREMOVED:
                var removeController = registController[e.cdevice.which];

                gameControllers.Remove(removeController);
                registController.Remove(removeController);

                SDL.SDL_GameControllerClose(e.cdevice.which);
                break;

            case SDL.SDL_EventType.SDL_QUIT:
                foreach (var controller in gameControllers)
                    SDL.SDL_GameControllerClose(controller.Key);

                gameControllers.Clear();
                registController.Clear();
                break;
        }
    }

    public static void Update()
    {
        foreach (var controller in gameControllers)
        {
            for (int i = 0; i < GAMECONTROLLER_BUTTON_NUM; i++)
            {
                if (SDL.SDL_GameControllerGetButton(controller.Key, GetGameController(i)) == 1)
                    controller.Value[i] = (sbyte)(IsPushing(controller.Key, GetGameController(i)) ? 2 : 1);
                else
                    controller.Value[i] = (sbyte)(IsPushing(controller.Key, GetGameController(i)) ? -1 : 0);
            }
        }
    }

    public static IReadOnlyList<nint>? GetRegisteredGameController()
    {
        if(!registController.Any())
            return null;

        return registController;
    }

    public static bool IsPushing(nint gameController, SDL.SDL_GameControllerButton button)
        => gameControllers[gameController][GetGameControllerIndex(button)] > 0;

    public static bool IsPushed(nint gameController, SDL.SDL_GameControllerButton button)
        => gameControllers[gameController][GetGameControllerIndex(button)] == 1;

    public static bool IsSeparate(nint gameController, SDL.SDL_GameControllerButton button)
        => gameControllers[gameController][GetGameControllerIndex(button)] == -1;

    private static int GetGameControllerIndex(SDL.SDL_GameControllerButton button)
        => (int)button;

    private static SDL.SDL_GameControllerButton GetGameController(int index)
        => index >= -1 && index <= GAMECONTROLLER_BUTTON_NUM
            ? (SDL.SDL_GameControllerButton)index
            : SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_INVALID;
}