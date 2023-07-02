using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Graphics;
using SpaceInvaders.Resource;

namespace SpaceInvaders.Scenes.Game;

internal class PlayerController : Scene
{
    private readonly Player player;
    private readonly PlayerMove move;
    private readonly PlayerAttack attack;
    private readonly PlayerInfo playerInfo;

    public PlayerController()
    {
        playerInfo = new()
        {
            MoveSpeed = 200,
            BeamSpeed = 600.0f,
            PlayerScale = 5.0f,
            PlayerBeamScale = 2.0f,
        };

        var playerSprite = SpriteManager.GetResource("Player");
        playerSprite.HorizontalScale = playerInfo.PlayerScale;
        playerSprite.VerticalScale = playerInfo.PlayerScale;

        var beamSprite = SpriteManager.GetResource("PlayerBeam");
        beamSprite.HorizontalScale = playerInfo.PlayerBeamScale;
        beamSprite.VerticalScale = playerInfo.PlayerBeamScale;

        player = new(playerSprite);
        move = new(player, playerInfo);
        attack = new(player, playerInfo, beamSprite);
    }

    public override void Init()
    {
        player.Init();
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

    public CollisionComponent GetCollision()
        => player.Collision;
}