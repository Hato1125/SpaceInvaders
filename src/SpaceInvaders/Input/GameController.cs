using SpaceInvaders.Logger;
using System.Runtime.CompilerServices;

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
            case SDL.SDL_EventType.SDL_CONTROLLERDEVICEADDED:
                AddController(e);
                break;

            case SDL.SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:
                RemoveController(e);
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
                    controller.Value[i] = (sbyte)(IsControllerPushing(controller.Key, GetGameController(i)) ? 2 : 1);
                else
                    controller.Value[i] = (sbyte)(IsControllerPushing(controller.Key, GetGameController(i)) ? -1 : 0);
            }
        }
    }

    public static IReadOnlyList<nint>? GetRegisteredGameController()
    {
        if (!registController.Any())
            return null;

        return registController;
    }

    public static bool IsControllerPushing(nint gameController, SDL.SDL_GameControllerButton button)
        => gameControllers.ContainsKey(gameController)
            && gameControllers[gameController][GetGameControllerIndex(button)] > 0;

    public static bool IsControllerPushed(nint gameController, SDL.SDL_GameControllerButton button)
        => gameControllers.ContainsKey(gameController)
            && gameControllers[gameController][GetGameControllerIndex(button)] == 1;

    public static bool IsControllerSeparate(nint gameController, SDL.SDL_GameControllerButton button)
        => gameControllers.ContainsKey(gameController)
            && gameControllers[gameController][GetGameControllerIndex(button)] == -1;

    public static bool IsPushing(int controllerIndex, SDL.SDL_GameControllerButton button)
        => registController.Any()
        && registController.Count - 1 >= controllerIndex
        && IsControllerPushing(registController[controllerIndex], button);

    public static bool IsPushed(int controllerIndex, SDL.SDL_GameControllerButton button)
        => registController.Any()
        && registController.Count - 1 >= controllerIndex
        && IsControllerPushed(registController[controllerIndex], button);

    public static bool IsSeparate(int controllerIndex, SDL.SDL_GameControllerButton button)
        => registController.Any()
        && registController.Count - 1 >= controllerIndex
        && IsControllerSeparate(registController[controllerIndex], button);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPushing(int controllerIndex, ReadOnlySpan<SDL.SDL_GameControllerButton> buttons)
    {
        foreach (var btn in buttons)
        {
            if (IsPushing(controllerIndex, btn))
                return true;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPushed(in int controllerIndex, ReadOnlySpan<SDL.SDL_GameControllerButton> buttons)
    {
        foreach (var btn in buttons)
        {
            if (IsPushed(controllerIndex, btn))
                return true;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSeparate(in int controllerIndex, ReadOnlySpan<SDL.SDL_GameControllerButton> buttons)
    {
        foreach (var btn in buttons)
        {
            if (IsSeparate(controllerIndex, btn))
                return true;
        }

        return false;
    }

    private static int GetGameControllerIndex(SDL.SDL_GameControllerButton button)
        => (int)button;

    private static SDL.SDL_GameControllerButton GetGameController(int index)
        => index >= -1 && index <= GAMECONTROLLER_BUTTON_NUM
            ? (SDL.SDL_GameControllerButton)index
            : SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_INVALID;

    private static void AddController(in SDL.SDL_Event e)
    {
        var addController = SDL.SDL_GameControllerOpen(e.cdevice.which);
        if (addController == nint.Zero)
            return;

        var contName = SDL.SDL_GameControllerName(addController);
        var name = string.IsNullOrWhiteSpace(contName) ? "Null" : contName;
        Log.WriteInfo($"[ADDCONTROLLER] Info\n\tName: {name}\n\tHandle: {addController}");

        registController.Add(addController);
        gameControllers.Add(addController, new sbyte[GAMECONTROLLER_BUTTON_NUM]);
    }

    private static void RemoveController(in SDL.SDL_Event e)
    {
        var instanceID = SDL.SDL_GameControllerFromInstanceID(e.cdevice.which);
        var removeIndex = registController.IndexOf(instanceID);

        var contName = SDL.SDL_GameControllerName(instanceID);
        var name = string.IsNullOrWhiteSpace(contName) ? "Null" : contName;
        Log.WriteInfo($"[REMOVECONTROLLER] Info\n\tName: {name}\n\tHandle: {instanceID}");

        gameControllers.Remove(instanceID);
        registController.Remove(removeIndex);

        SDL.SDL_GameControllerClose(instanceID);
    }
}