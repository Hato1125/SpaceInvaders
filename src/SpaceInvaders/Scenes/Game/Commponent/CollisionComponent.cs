using System.Runtime.CompilerServices;

namespace SpaceInvaders.Scenes.Game;

internal class CollisionComponent
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCollision(CollisionComponent collision1, CollisionComponent collision2)
        => collision1.X + collision1.Width >= collision2.X
        && collision1.X <= collision2.X + collision2.Width
        && collision1.Y + collision1.Height >= collision2.Y
        && collision1.Y <= collision2.Y + collision2.Height;
}