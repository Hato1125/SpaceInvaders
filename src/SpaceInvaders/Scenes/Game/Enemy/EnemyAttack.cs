using SpaceInvaders.Graphics;
using System.Runtime.CompilerServices;

namespace SpaceInvaders.Scenes.Game;

internal class EnemyAttack
{
    private readonly EnemyInfo enemyInfo;
    private readonly Enemy[,] enemyCell;
    private readonly Enemy?[] attackEnemys;

    private Sprite? beamSprite;

    private int attackEnemyNum;
    private double attackInterval;
    private double attackIntervalCounter;

    public EnemyAttack(Enemy[,] enemys, EnemyInfo info)
    {
        enemyCell = enemys;
        enemyInfo = info;
        attackEnemys = new Enemy?[info.ColumnNum];
    }

    public void Init(Sprite beam)
    {
        beamSprite = beam;
        attackIntervalCounter = 0;
        attackInterval = App.App.Random.Next(
            enemyInfo.AttackIntervalMin,
            enemyInfo.AttackIntervalMax + 1
        );
    }

    public void Update()
    {
        AttackableEnemySearch();
        Attack();
    }

    private void Attack()
    {
        if (!attackEnemys.Any())
            return;

        attackIntervalCounter += App.App.Window.DeltaTime;
        if (attackIntervalCounter > attackInterval && beamSprite != null)
        {
            attackIntervalCounter = 0;
            attackInterval = App.App.Random.Next(
                enemyInfo.AttackIntervalMin,
                enemyInfo.AttackIntervalMax + 1
            );

            var attackIndex = App.App.Random.Next(0, attackEnemyNum);
            var attackEnemy = attackEnemys[attackIndex];
            if (attackEnemy == null)
                return;

            var beginX = attackEnemy.X + (attackEnemy.Collision.Width - beamSprite.ActualWidth) / 2;
            var beginY = attackEnemy.Y;

            new EnemyBeam(beginX, beginY, enemyInfo.BeamSpeed, beamSprite);
        }
    }

    private void AttackableEnemySearch()
    {
        attackEnemyNum = 0;
        Array.Clear(attackEnemys);
        for (int i = 0; i < enemyInfo.ColumnNum; i++)
        {
            for (int j = enemyInfo.RowNum - 1; j >= 0; j--)
            {
                if (!enemyCell[j, i].IsDead)
                {
                    attackEnemys[attackEnemyNum] = enemyCell[j, i];
                    attackEnemyNum++;
                    break;
                }
            }
        }
    }
}