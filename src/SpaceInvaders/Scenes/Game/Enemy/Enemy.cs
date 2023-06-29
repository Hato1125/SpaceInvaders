using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class Enemy
{
    public readonly CollisionComponent Collision;
    public readonly Sprite[] AnimeSprites;
    public readonly int Point;

    public float X { get; set; }
    public float Y { get; set; }
    public bool IsDead { get; set; }

    private int animeIndex;
    public int AnimeIndex
    {
        get => animeIndex;
        set
        {
            if (value > AnimeSprites.Length - 1)
                animeIndex = 0;
            else if (value < 0)
                animeIndex = 0;
            else
                animeIndex = value;
        }
    }

    public Enemy(Sprite[] sprites, int point)
    {
        Collision = new();
        Point = point;
        AnimeSprites = sprites;
        IsDead = false;
    }

    public void Update()
    {
        if (IsDead)
            return;

        Collision.X = X;
        Collision.Y = Y;
        Collision.Width = AnimeSprites[AnimeIndex].ActualWidth;
        Collision.Height = AnimeSprites[AnimeIndex].ActualHeight;
    }

    public void Render()
    {
        if (IsDead)
            return;

        AnimeSprites[AnimeIndex].Render(X, Y);
    }
}