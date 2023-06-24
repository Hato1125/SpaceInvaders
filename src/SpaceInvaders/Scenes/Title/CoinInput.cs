using SpaceInvaders.Input;
using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Title;

internal class CoinInput
{
    private readonly SDL.SDL_GameControllerButton[] buttons = new SDL.SDL_GameControllerButton[]
    {
        SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A,
        SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_B,
    };

    private readonly SDL.SDL_GameControllerButton[] nextSceneButtons = new SDL.SDL_GameControllerButton[]
{
        SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X,
        SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y,
};

    private readonly SDL.SDL_Scancode[] keyCodes = new SDL.SDL_Scancode[]
    {
        SDL.SDL_Scancode.SDL_SCANCODE_P,
        SDL.SDL_Scancode.SDL_SCANCODE_PAGEUP,
    };

    public void Update()
    {
        var controller = GameController.GetRegisteredGameController();

        if (IsKeyboardPushed(keyCodes) || IsControllerPushed(controller, buttons))
            CoinManager.PushCoin();

        if (CoinManager.Coin > 0)
        {
            if (Keyboard.IsPushed(SDL.SDL_Scancode.SDL_SCANCODE_SPACE)
                || IsControllerPushed(controller, nextSceneButtons))
            {
                CoinManager.DecreCoin();
                SceneManager.ChangeScene("Game");
            }
        }
    }

    private static bool IsControllerPushed(IReadOnlyList<nint>? controller, SDL.SDL_GameControllerButton[] buttons)
    {
        if (controller == null)
            return false;

        foreach (var button in buttons)
        {
            if (GameController.IsPushed(controller[0], button))
                return true;
        }

        return false;
    }

    private static bool IsKeyboardPushed(SDL.SDL_Scancode[] keyCodes)
    {
        foreach (var keyCode in keyCodes)
        {
            if (Keyboard.IsPushed(keyCode))
                return true;
        }

        return false;
    }
}