namespace SpaceInvaders.Scenes.Game;

internal class EnemyInfo
{
    public int Kinds { get; init; }
    public int AnimeNum { get; init; }
    public int RowNum { get; init; }
    public int ColumnNum { get; init; }
    public int EnemyMoveNum { get; init; }
    public int EnemyMovePixel { get; init; }
    public int EnemyInterval { get; init; }
    public int AttackIntervalMin { get; init; }
    public int AttackIntervalMax { get; init; }
    public float BeamSpeed { get; init; }
    public float EnemyScale { get; init; }
    public float EnemyBeamScale { get; init; }
    public float BeginMoveInterbal { get; init; }
    public float DecreaseMoveInterbal { get; init; }
}