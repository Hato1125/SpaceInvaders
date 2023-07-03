using SpaceInvaders.Frame;
using SpaceInvaders.Resource;

namespace SpaceInvaders.Scenes.Game;

internal class EnemyController : Scene
{
    private readonly EnemyInfo enemyInfo;
    private readonly Enemy[,] enemyCell;

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
            AttackIntervalMin = 1,
            AttackIntervalMax = 2,
            BeamSpeed = 400.0f,
            EnemyScale = 2.5f,
            EnemyBeamScale = 2.0f,
            BeginMoveInterbal = 1.0f,
            DecreaseMoveInterbal = 0.085f,
        };

        enemyCell = new Enemy[enemyInfo.RowNum, enemyInfo.ColumnNum];

        var move = new EnemyMove(enemyCell, enemyInfo);
        var attack = new EnemyAttack(enemyCell, enemyInfo, SpriteManager.GetResource("EnemyBeam"));

        Elements.Add(move);
        Elements.Add(attack);
    }

    public override void Init()
    {
        for (int i = 0; i < enemyInfo.RowNum; i++)
        {
            for (int j = 0; j < enemyInfo.ColumnNum; j++)
            {
                var sprites = SpriteManager.GetResource(GetEnemySpriteName(i));
                var point = GetEnemyPoint(i);

                foreach (var sprite in sprites)
                {
                    sprite.HorizontalScale = enemyInfo.EnemyScale;
                    sprite.VerticalScale = enemyInfo.EnemyScale;
                }

                enemyCell[i, j] = new(sprites, point);
            }
        }

        var beam = SpriteManager.GetResource("EnemyBeam");
        beam.HorizontalScale = enemyInfo.EnemyBeamScale;
        beam.VerticalScale = enemyInfo.EnemyBeamScale;

        base.Init();
    }

    public override void Update()
    {
        base.Update();

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

    public Enemy[,] GetEnemyCell()
        => enemyCell;

    private static string[] GetEnemySpriteName(int index) => index switch
    {
        0 => new string[] { "Enemy0_0", "Enemy0_1" },
        1 or 2 => new string[] { "Enemy1_0", "Enemy1_1" },
        3 or 4 => new string[] { "Enemy2_0", "Enemy2_1" },
        _ => new string[] { "Enemy0_0", "Enemy0_1" },
    };

    private static int GetEnemyPoint(int index) => index switch
    {
        0 => 10,
        1 or 2 => 20,
        3 or 4 => 30,
        _ => 0,
    };
}