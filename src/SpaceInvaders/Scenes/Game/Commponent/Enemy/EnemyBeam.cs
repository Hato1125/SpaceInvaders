using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class EnemyBeam
{
    private readonly Sprite beamSprite;
    public readonly CollisionComponent Collision;

    private float x;
    private float y;

    public EnemyBeam(float beginX, float beginY, Sprite beam)
    {
        x = beginX;
        y = beginY;
        Collision = new();
        beamSprite = beam;

        BeamScreen.EnemyBeam.Add(this);
    }

    public void Update()
    {
        y += (float)(400 * App.App.Window.DeltaTime);

        Collision.X = x;
        Collision.Y = y;
        Collision.Width = beamSprite.ActualWidth;
        Collision.Height = beamSprite.ActualHeight;

        if(CollisionComponent.IsCollision(Collision, GameScene.Player.Collision))
            BeamScreen.EnemyBeam.Remove(this);
    }

    public void Render()
    {
        beamSprite.Render(x, y);
    }
}