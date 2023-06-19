namespace SpaceInvaders.Scenes.Game;

internal class CollisionComponent
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }

    public static bool IsCollision(CollisionComponent collision1, CollisionComponent collision2)
    {
        if (collision1.X + collision1.Width > collision2.X
            && collision1.X < collision2.X + collision2.Width)
        {
            if (collision1.Y + collision1.Height > collision2.Y
                && collision1.Y < collision2.Y + collision2.Height)
                return true;
        }

        return false;
    }
}