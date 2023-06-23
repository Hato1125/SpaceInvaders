using SpaceInvaders.Input;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class PlayerAttack
{
    private readonly Player player;
    private readonly PlayerInfo playerInfo;

    private Sprite? beamSprite;

    public PlayerAttack(Player player, PlayerInfo info)
    {
        this.player = player;
        playerInfo = info;
    }

    public void Init(Sprite beam)
    {
        beamSprite = beam;
    }

    public void Update()
    {
        if(beamSprite == null)
            return;

        var controllers = GameController.GetRegisteredGameController();

        if(IsAttackPush(controllers) && BeamScreen.PlayerBeam == null)
        {
            var beginX = player.X + (player.Collision.Width - beamSprite.ActualWidth) / 2;
            var beginY = player.Y;

            new PlayerBeam(beginX, beginY, playerInfo.BeamSpeed, beamSprite);
        }
    }

    private bool IsAttackPush(IReadOnlyList<nint>? controllers)
    {
        if (Keyboard.IsPushing(SDL.SDL_Scancode.SDL_SCANCODE_SPACE))
        {
            return true;
        }
        else
        {
            if (controllers == null)
                return false;

            if (GameController.IsPushing(
                controllers[0],
                SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A))
                return true;
        }

        return false;
    }
}