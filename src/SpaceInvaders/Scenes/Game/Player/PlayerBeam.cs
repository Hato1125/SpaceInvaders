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

        GameScene.BeamScreen.PushBeam(this);
    }

    public void Update()
    {
        y -= (float)(beamSpeed * App.App.Window.DeltaTime);

        Collision.X = x;
        Collision.Y = y;
        Collision.Width = beamSprite.ActualWidth;
        Collision.Height = beamSprite.ActualHeight;

        if (y < -beamSprite.ActualHeight)
        {
            GameScene.BeamScreen.RemoveBeam();
        }
        else
        {
            var enemyCell = GameScene.Enemy.GetEnemyCell();
            for (int i = 0; i < enemyCell.GetLength(0); i++)
            {
                for (int j = 0; j < enemyCell.GetLength(1); j++)
                {
                    if (!enemyCell[i, j].IsDead
                        && CollisionComponent.IsCollision(enemyCell[i, j].Collision, Collision))
                    {
                        enemyCell[i, j].IsDead = true;
                        GameScene.BeamScreen.RemoveBeam();

                        CheckGameClear();
                    }
                }
            }
        }
    }

    public void Render()
    {
        beamSprite.Render(x, y);
    }

    private void CheckGameClear()
    {
        if (GameScene.GameClear.IsGameClear)
            return;

        var enemyCell = GameScene.Enemy.GetEnemyCell();
        var enemyArray = enemyCell.Cast<Enemy>();

        GameScene.GameClear.IsGameClear = enemyArray.All(e => e.IsDead);
    }
}