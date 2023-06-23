using SpaceInvaders.Input;

namespace SpaceInvaders.Scenes.Game;

internal class PlayerMove
{
    private readonly Player player;
    private readonly PlayerInfo playerInfo;

    public PlayerMove(Player player, PlayerInfo info)
    {
        this.player = player;
        playerInfo = info;
    }

    public void Update()
    {
        var controllers = GameController.GetRegisteredGameController();

        if(IsLeftPush(controllers))
            player.X -= (float)(playerInfo.MoveSpeed * App.App.Window.DeltaTime);

        if(IsRightPush(controllers))
            player.X += (float)(playerInfo.MoveSpeed * App.App.Window.DeltaTime);
    }

    private bool IsLeftPush(IReadOnlyList<nint>? controllers)
    {
        if (Keyboard.IsPushing(SDL.SDL_Scancode.SDL_SCANCODE_LEFT))
        {
            return true;
        }
        else
        {
            if (controllers == null)
                return false;

            if (GameController.IsPushing(
                controllers[0],
                SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT))
                return true;
        }

        return false;
    }

    private bool IsRightPush(IReadOnlyList<nint>? controllers)
    {
        if (Keyboard.IsPushing(SDL.SDL_Scancode.SDL_SCANCODE_RIGHT))
        {
            return true;
        }
        else
        {
            if (controllers == null)
                return false;

            if (GameController.IsPushing(
                controllers[0],
                SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT))
                return true;
        }

        return false;
    }
}