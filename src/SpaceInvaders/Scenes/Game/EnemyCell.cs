using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class EnemyCell : Scene
{
    private const int ENEMY_KINDS = 3;
    private const int ENEMY_ANIME = 2;
    private const int ENEMY_ROW = 5;
    private const int ENEMY_COLUMN = 11;
    private const int ENEMY_INTERVAL = 38;
    private const int ENEMY_VERTICAL_PERCENT = 20;
    private const float ENEMY_SCALE = 2.5f;

    private const int MOVE_NUM = 18;
    private const int MOVE_PIXEL = 10;
    private const float MOVE_START_INTERVAL = 1.0f;
    private const float MOVE_DECELERATION = 0.1f;

    private readonly Sprite[,] enemySprites = new Sprite[ENEMY_KINDS, ENEMY_ANIME];
    private static readonly EnemyComponent[,] enemys = new EnemyComponent[ENEMY_ROW, ENEMY_COLUMN];

    private int movedCount;
    private double moveCounter;
    private double moveInterval;
    private char animeIndex;
    private bool isDown;
    private bool isLeftMove;

    public override void Init()
    {
        moveCounter = 0;
        movedCount = MOVE_NUM / 2;
        moveInterval = MOVE_START_INTERVAL;

        for (int i = 0; i < ENEMY_KINDS; i++)
        {
            for (int j = 0; j < ENEMY_ANIME; j++)
            {
                enemySprites[i, j] = new Sprite(
                    App.App.Window.RendererPtr,
                    $"{AppInfo.TextureDire}Game\\Enemy_{i}\\{j}.png"
                )
                {
                    HorizontalScale = ENEMY_SCALE,
                    VerticalScale = ENEMY_SCALE,
                };
            }
        }

        for (int i = 0; i < ENEMY_ROW; i++)
        {
            for (int j = 0; j < ENEMY_COLUMN; j++)
            {
                var sprites = i switch
                {
                    0 => new Sprite[] { enemySprites[2, 0], enemySprites[2, 1] },
                    1 => new Sprite[] { enemySprites[1, 0], enemySprites[1, 1] },
                    2 => new Sprite[] { enemySprites[1, 0], enemySprites[1, 1] },
                    3 => new Sprite[] { enemySprites[1, 0], enemySprites[1, 1] },
                    4 => new Sprite[] { enemySprites[0, 0], enemySprites[0, 1] },
                    _ => new Sprite[] { enemySprites[2, 0], enemySprites[2, 1] }
                };

                var positionX = (AppInfo.Width - ENEMY_INTERVAL * ENEMY_COLUMN) / 2 + j * ENEMY_INTERVAL;
                var positionY = (AppInfo.Height / 100) * ENEMY_VERTICAL_PERCENT + ENEMY_INTERVAL * i;

                enemys[i, j] = new(sprites)
                {
                    X = positionX,
                    Y = positionY,
                };
                enemys[i, j].Update();
            }
        }
    }

    public override void Update()
    {
        moveCounter += App.App.Window.DeltaTime;

        if (moveCounter > moveInterval)
        {
            moveCounter = 0;

            animeIndex = (char)(animeIndex > 0 ? 0 : 1);

            if (!isDown)
                movedCount++;

            for (int i = 0; i < ENEMY_ROW; i++)
            {
                for (int j = 0; j < ENEMY_COLUMN; j++)
                {
                    enemys[i, j].AnimeIndex = animeIndex;

                    if (isDown)
                        enemys[i, j].Y += ENEMY_INTERVAL;
                    else
                        enemys[i, j].X += isLeftMove ? -MOVE_PIXEL : MOVE_PIXEL;

                    enemys[i, j].Update();
                }
            }

            if (isDown)
            {
                isDown = false;
                moveInterval -= MOVE_DECELERATION;
            }

            if (movedCount >= MOVE_NUM)
            {
                movedCount = 0;
                isDown = true;
                isLeftMove = !isLeftMove;
            }
        }
    }

    public override void Render()
    {
        for (int i = 0; i < ENEMY_ROW; i++)
        {
            for (int j = 0; j < ENEMY_COLUMN; j++)
                enemys[i, j].Render();
        }
    }

    public override void Finish()
    {
        for (int i = 0; i < ENEMY_KINDS; i++)
        {
            for (int j = 0; j < ENEMY_ANIME; j++)
                enemySprites[i, j].Dispose();
        }
    }

    public static EnemyComponent[,] GetEnemys()
        => enemys;
}