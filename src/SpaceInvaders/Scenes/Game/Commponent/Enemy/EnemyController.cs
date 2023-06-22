using SpaceInvaders.App;
using SpaceInvaders.Frame;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class EnemyController : Scene
{
    private readonly EnemyMove move;

    private readonly EnemyInfo enemyInfo;
    private readonly Enemy[,] enemyCell;
    private readonly Sprite[,] enemySprites;

    public EnemyController()
    {
        enemyInfo = new()
        {
            Kinds = 3,
            AnimeNum = 2,
            RowNum = 5,
            ColumnNum = 11,
            EnemyMoveNum = 20,
            EnemyMovePixel = 8,
            EnemyInterval = 40,
            EnemyScale = 2.5f,
            BeginMoveInterbal = 1.0f,
            DecreaseMoveInterbal = 0.085f,
        };

        enemyCell = new Enemy[enemyInfo.RowNum, enemyInfo.ColumnNum];
        enemySprites = new Sprite[enemyInfo.Kinds, enemyInfo.AnimeNum];

        move = new(enemyCell, enemyInfo);
    }

    public override void Init()
    {
        for (int i = 0; i < enemyInfo.Kinds; i++)
        {
            for (int j = 0; j < enemyInfo.AnimeNum; j++)
            {
                var renderer = App.App.Window.RendererPtr;
                var fileName = $"{AppInfo.GameTextureDire}Enemy_{i}\\{j}.png";

                enemySprites[i, j] = new(renderer, fileName)
                {
                    HorizontalScale = enemyInfo.EnemyScale,
                    VerticalScale = enemyInfo.EnemyScale,
                };
            }
        }

        for (int i = 0; i < enemyInfo.RowNum; i++)
        {
            for (int j = 0; j < enemyInfo.ColumnNum; j++)
                enemyCell[i, j] = new(GetEnemySprites(i));
        }

        move.Init();
    }

    public override void Update()
    {
        move.Update();

        for (int i = 0; i < enemyInfo.RowNum; i++)
        {
            for (int j = 0; j < enemyInfo.ColumnNum; j++)
                enemyCell[i, j].Update();
        }
    }

    public override void Render()
    {
        for (int i = 0; i < enemyInfo.RowNum; i++)
        {
            for (int j = 0; j < enemyInfo.ColumnNum; j++)
                enemyCell[i, j].Render();
        }
    }

    public override void Finish()
    {
        for (int i = 0; i < enemyInfo.Kinds; i++)
        {
            for (int j = 0; j < enemyInfo.AnimeNum; j++)
                enemySprites[i, j].Dispose();
        }
    }

    private Sprite[] GetEnemySprites(int index) => index switch
    {
        0 => new Sprite[] { enemySprites[0, 0], enemySprites[0, 1] },
        1 or 2 => new Sprite[] { enemySprites[1, 0], enemySprites[1, 1] },
        3 or 4 => new Sprite[] { enemySprites[2, 0], enemySprites[2, 1] },
        _ => new Sprite[] { enemySprites[0, 0], enemySprites[0, 1] },
    };
}