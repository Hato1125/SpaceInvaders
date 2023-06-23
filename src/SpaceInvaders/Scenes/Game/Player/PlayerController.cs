using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class PlayerController : Scene
{
    private readonly Player player;
    private readonly PlayerMove move;
    private readonly PlayerAttack attack;
    private readonly PlayerInfo playerInfo;

    private Sprite? playerSprite;
    private Sprite? beamSprite;

    public PlayerController()
    {
        playerInfo = new()
        {
            MoveSpeed = 200,
            BeamSpeed = 600.0f,
            PlayerScale = 5.0f,
            PlayerBeamScale = 2.0f,
        };

        player = new();
        move = new(player, playerInfo);
        attack = new(player, playerInfo);
    }

    public override void Init()
    {
        playerSprite = new(App.App.Window.RendererPtr, $"{AppInfo.GameTextureDire}Player.png")
        {
            HorizontalScale = playerInfo.PlayerScale,
            VerticalScale = playerInfo.PlayerScale,
        };

        beamSprite = new(App.App.Window.RendererPtr, $"{AppInfo.GameTextureDire}PlayerBeam.png")
        {
            HorizontalScale = playerInfo.PlayerBeamScale,
            VerticalScale = playerInfo.PlayerBeamScale,
        };

        player.Init(playerSprite);
        attack.Init(beamSprite);
    }

    public override void Update()
    {
        player.Update();
        move.Update();
        attack.Update();
    }

    public override void Render()
    {
        player.Render();
    }

    public override void Finish()
    {
        playerSprite?.Dispose();
    }

    public CollisionComponent GetCollision()
        => player.Collision;
}