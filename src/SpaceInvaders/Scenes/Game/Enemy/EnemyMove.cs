using SpaceInvaders.App;

namespace SpaceInvaders.Scenes.Game;

internal class EnemyMove
{
    private readonly EnemyInfo enemyInfo;
    private readonly Enemy[,] enemyCell;

    private int beginX;
    private int beginY;
    private int moveX;
    private int moveY;
    private int moveCount;
    private bool isDown;
    private bool isLeftMove;
    private double interval;
    private double intervalCounter;

    public EnemyMove(Enemy[,] enemys, EnemyInfo info)
    {
        enemyInfo = info;
        enemyCell = enemys;
        interval = info.BeginMoveInterbal;
        moveCount = info.EnemyMoveNum / 2;
        beginX = (AppInfo.Width - info.EnemyInterval * info.ColumnNum) / 2;
        beginY = (AppInfo.Height / 100) * 20;
    }

    public void Init()
    {
        interval = enemyInfo.BeginMoveInterbal;
        moveCount = enemyInfo.EnemyMoveNum / 2;

        for (int i = 0; i < enemyInfo.RowNum; i++)
        {
            for (int j = 0; j < enemyInfo.ColumnNum; j++)
            {
                enemyCell[i, j].X = j * enemyInfo.EnemyInterval + beginX + moveX;
                enemyCell[i, j].Y = i * enemyInfo.EnemyInterval + beginY + moveY;
            }
        }
    }

    public void Update()
    {
        intervalCounter += App.App.Window.DeltaTime;
        if (intervalCounter > interval)
        {
            intervalCounter = 0;

            if (isDown)
            {
                moveY += enemyInfo.EnemyInterval;
                interval -= enemyInfo.DecreaseMoveInterbal;
                isDown = false;
            }
            else
            {
                moveX += isLeftMove
                    ? -enemyInfo.EnemyMovePixel
                    : enemyInfo.EnemyMovePixel;

                if (moveCount == enemyInfo.EnemyMoveNum - 1)
                {
                    moveCount = 0;
                    isDown = true;
                    isLeftMove = !isLeftMove;
                }
                else
                {
                    moveCount++;
                }
            }

            for (int i = 0; i < enemyInfo.RowNum; i++)
            {
                for (int j = 0; j < enemyInfo.ColumnNum; j++)
                {
                    enemyCell[i, j].AnimeIndex++;
                    enemyCell[i, j].X = j * enemyInfo.EnemyInterval + beginX + moveX;
                    enemyCell[i, j].Y = i * enemyInfo.EnemyInterval + beginY + moveY;
                }
            }
        }
    }
}