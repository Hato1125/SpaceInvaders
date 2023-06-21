using SpaceInvaders.App;
using SpaceInvaders.Input;
using SpaceInvaders.Frame;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class Player : Scene
{
    private const float PLAYER_SPEED = 200f;
    private const float PLAYER_SCALE = 5f;
    private const float PLAYER_VERTICAL_PERCENT = 80f;
    private const float PLAYER_BEAM_SCALE = 2f;

    private Sprite? playerSprite;
    private Sprite? beamSprite;
    private float playerX;
    private float playerY;

    public override void Init()
    {
        playerSprite = new(App.App.Window.RendererPtr, $"{AppInfo.TextureDire}Game\\Player.png")
        {
            HorizontalScale = PLAYER_SCALE,
            VerticalScale = PLAYER_SCALE,
        };

        beamSprite = new(App.App.Window.RendererPtr, $"{AppInfo.TextureDire}Game\\PlayerBeam.png")
        {
            HorizontalScale = PLAYER_BEAM_SCALE,
            VerticalScale = PLAYER_BEAM_SCALE,
        };

        playerX = (AppInfo.Width - playerSprite.ActualWidth) / 2;
        playerY = (AppInfo.Height / 100) * PLAYER_VERTICAL_PERCENT;
    }

    public override void Update()
    {
        var controller = GameController.GetRegisteredGameController();

        if (controller != null)
        {
            if (GameController.IsPushing(controller[0], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT)
                || Keyboard.IsPushing(SDL.SDL_Scancode.SDL_SCANCODE_LEFT))
                playerX -= (float)(PLAYER_SPEED * App.App.Window.DeltaTime);

            if (GameController.IsPushing(controller[0], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT)
                || Keyboard.IsPushing(SDL.SDL_Scancode.SDL_SCANCODE_RIGHT))
                playerX += (float)(PLAYER_SPEED * App.App.Window.DeltaTime);

            if (GameController.IsPushed(controller[0], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A)
                || Keyboard.IsPushed(SDL.SDL_Scancode.SDL_SCANCODE_SPACE))
            {
                if (BeamScreen.PlayerBeam == null
                    && playerSprite != null
                    && beamSprite != null)
                {
                    var beginX = playerX + (playerSprite.ActualWidth - beamSprite.ActualWidth) / 2;
                    var beginY = playerY;
                    new PlayerBeamComponent(beamSprite, beginX, beginY);
                }
            }
        }
    }

    public override void Render()
    {
        playerSprite?.Render(playerX, playerY);
    }

    public override void Finish()
    {
        playerSprite?.Dispose();
    }
}