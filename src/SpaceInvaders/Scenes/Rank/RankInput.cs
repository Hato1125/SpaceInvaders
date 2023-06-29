using SpaceInvaders.Frame;
using SpaceInvaders.Input;

namespace SpaceInvaders.Scenes.Rank;

internal class RankInput : Scene
{
    public override void Update()
    {
        var controllers = GameController.GetRegisteredGameController();

        if (IsDownList(controllers))
            RankScene.RankList.DownRankList();

        if (IsUpList(controllers))
            RankScene.RankList.UpRankList();
    }

    private bool IsUpList(IReadOnlyList<nint>? controllers)
    {
        if (Keyboard.IsPushing(SDL.SDL_Scancode.SDL_SCANCODE_UP))
        {
            return true;
        }
        else
        {
            if (controllers == null)
                return false;

            if (GameController.IsPushing(
                controllers[0],
                SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_UP))
                return true;
        }

        return false;
    }

    private bool IsDownList(IReadOnlyList<nint>? controllers)
    {
        if (Keyboard.IsPushing(SDL.SDL_Scancode.SDL_SCANCODE_DOWN))
        {
            return true;
        }
        else
        {
            if (controllers == null)
                return false;

            if (GameController.IsPushing(
                controllers[0],
                SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_DOWN))
                return true;
        }

        return false;
    }
}