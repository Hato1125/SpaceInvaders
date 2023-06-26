using LiteDB;

namespace SpaceInvaders.Database;

internal class ScoreData
{
    public ObjectId? Id { get; set; }
    public string? Name { get; set; }
    public int Score { get; set; }
}