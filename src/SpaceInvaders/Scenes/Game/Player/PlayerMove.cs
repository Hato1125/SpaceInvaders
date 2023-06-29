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
        if(Keyboard.IsPushing(SDL.SDL_Scancode.SDL_SCANCODE_LEFT)
            || GameController.IsPushing(0, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT))
            player.X -= (float)(playerInfo.MoveSpeed * App.App.Window.DeltaTime);

        if (Keyboard.IsPushing(SDL.SDL_Scancode.SDL_SCANCODE_RIGHT)
            || GameController.IsPushing(0, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT))
            player.X += (float)(playerInfo.MoveSpeed * App.App.Window.DeltaTime);
    }
}