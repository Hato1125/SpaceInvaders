using SpaceInvaders.Input;
using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Title;

internal class CoinInput : SceneElement
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

    public override void Update()
    {
        if (Keyboard.IsPushed(keyCodes)
            || GameController.IsPushed(0, buttons))
            CoinManager.IncreCoin();

        if (CoinManager.Coin > 0)
        {
            if (Keyboard.IsPushed(SDL.SDL_Scancode.SDL_SCANCODE_SPACE)
                || GameController.IsPushed(0, nextSceneButtons))
            {
                CoinManager.DecreCoin();
                SceneManager.ChangeScene("Round");
            }
        }
    }
}