using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class PlayerBeam
{
    private readonly Sprite beamSprite;
    public readonly CollisionComponent Collision;

    private float x;
    private float y;
    private float beamSpeed;

    public PlayerBeam(float beginX, float beginY, float speed, Sprite beam)
    {
        x = beginX;
        y = beginY;
        beamSpeed = speed;
        Collision = new();
        beamSprite = beam;

        BeamScreen.PlayerBeam = this;
    }

    public void Update()
    {
        y -= (float)(beamSpeed * App.App.Window.DeltaTime);

        Collision.X = x;
        Collision.Y = y;
        Collision.Width = beamSprite.ActualWidth;
        Collision.Height = beamSprite.ActualHeight;

        if (y < -beamSprite.ActualHeight)
            BeamScreen.PlayerBeam = null;

        var enemyCell = GameScene.Enemy.GetEnemyCell();
        for (int i = 0; i < enemyCell.GetLength(0); i++)
        {
            for (int j = 0; j < enemyCell.GetLength(1); j++)
            {
                if (!enemyCell[i, j].IsDead
                    && CollisionComponent.IsCollision(enemyCell[i, j].Collision, Collision))
                {
                    enemyCell[i, j].IsDead = true;
                    BeamScreen.PlayerBeam = null;
                }
            }
        }
    }

    public void Render()
    {
        beamSprite.Render(x, y);
    }
}