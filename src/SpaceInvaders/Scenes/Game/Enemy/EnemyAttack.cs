using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class EnemyAttack
{
    private readonly EnemyInfo enemyInfo;
    private readonly Enemy[,] enemyCell;
    private readonly List<Enemy> attackEnemys;

    private Sprite? beamSprite;

    private double attackInterval;
    private double attackIntervalCounter;

    public EnemyAttack(Enemy[,] enemys, EnemyInfo info)
    {
        enemyCell = enemys;
        enemyInfo = info;
        attackEnemys = new(info.ColumnNum);
    }

    public void Init(Sprite beam)
    {
        beamSprite = beam;
        attackIntervalCounter = 0;
        attackInterval = App.App.Random.Next(1, 3);
    }

    public void Update()
    {
        AttackableEnemySearch();
        Attack();
    }

    private void Attack()
    {
        attackIntervalCounter += App.App.Window.DeltaTime;
        if (attackIntervalCounter > attackInterval && beamSprite != null)
        {
            attackIntervalCounter = 0;
            attackInterval = App.App.Random.Next(1, 2);

            var attackIndex = App.App.Random.Next(0, attackEnemys.Count);
            var attackEnemy = attackEnemys[attackIndex];
            var beginX = attackEnemy.X + (attackEnemy.Collision.Width - beamSprite.ActualWidth) / 2;
            var beginY = attackEnemy.Y;

            new EnemyBeam(beginX, beginY, beamSprite);
        }
    }

    private void AttackableEnemySearch()
    {
        attackEnemys.Clear();
        for (int i = 0; i < enemyInfo.ColumnNum; i++)
        {
            for (int j = enemyInfo.RowNum - 1; j > 0; j--)
            {
                if (j != 0)
                {
                    if (!enemyCell[j, i].IsDead)
                    {
                        attackEnemys.Add(enemyCell[j, i]);
                        break;
                    }
                }
            }
        }
    }
}