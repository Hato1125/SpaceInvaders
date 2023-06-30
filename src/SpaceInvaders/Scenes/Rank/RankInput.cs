using SpaceInvaders.Frame;
using SpaceInvaders.Input;
using SDL2;

namespace SpaceInvaders.Scenes.Rank;

internal class RankInput : Scene
{
    public override void Update()
    {
        if (Keyboard.IsPushed(SDL.SDL_Scancode.SDL_SCANCODE_F))
            RankScene.IsStartFadeOut = true;

        if (!RankScene.IsStartFadeOut)
        {
            if (Keyboard.IsPushing(SDL.SDL_Scancode.SDL_SCANCODE_DOWN)
                || GameController.IsPushing(0, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_DOWN))
            {
                RankScene.RankList.DownRankList();
            }
            else if (Keyboard.IsPushing(SDL.SDL_Scancode.SDL_SCANCODE_UP)
                || GameController.IsPushing(0, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_UP))
            {
                RankScene.RankList.UpRankList();
            }
        }
    }
}