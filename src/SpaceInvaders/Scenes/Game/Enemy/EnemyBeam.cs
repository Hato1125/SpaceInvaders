using SpaceInvaders.App;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class EnemyBeam
{
    private readonly Sprite beamSprite;
    public readonly CollisionComponent Collision;

    private float x;
    private float y;
    private float beamSpeed;

    public EnemyBeam(float beginX, float beginY, float speed, Sprite beam)
    {
        x = beginX;
        y = beginY;
        beamSpeed = speed;
        Collision = new();
        beamSprite = beam;

        GameScene.BeamScreen.PushBeam(this);
    }

    public void Update()
    {
        y += (float)(beamSpeed * App.App.Window.DeltaTime);

        Collision.X = x;
        Collision.Y = y;
        Collision.Width = beamSprite.ActualWidth;
        Collision.Height = beamSprite.ActualHeight;

        if (y >= AppInfo.Height + beamSprite.ActualHeight)
            GameScene.BeamScreen.RemoveBeam(this);

        if (CollisionComponent.IsCollision(Collision, GameScene.Player.GetCollision()))
            GameScene.BeamScreen.RemoveBeam(this);
    }

    public void Render()
    {
        beamSprite.Render(x, y);
    }
}