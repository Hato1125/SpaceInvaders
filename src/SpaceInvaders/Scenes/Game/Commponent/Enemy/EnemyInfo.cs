namespace SpaceInvaders.Scenes.Game;

internal readonly struct EnemyInfo
{
    public int Kinds { get; init; }
    public int AnimeNum { get; init; }
    public int RowNum { get; init; }
    public int ColumnNum { get; init; }
    public int EnemyMoveNum { get; init; }
    public int EnemyMovePixel { get; init; }
    public int EnemyInterval { get; init; }
    public float EnemyScale { get; init; }
    public float BeginMoveInterbal { get; init; }
    public float DecreaseMoveInterbal { get; init; }
}