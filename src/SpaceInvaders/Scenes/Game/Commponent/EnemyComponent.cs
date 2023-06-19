using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class EnemyComponent
{
    private readonly CollisionComponent collisionComponent;
    private readonly Sprite[] animeSprites;
    private readonly float spriteScale;

    public float X { get; set; }
    public float Y { get; set; }
    public bool IsDead { get; set; }
    public bool IsCollision { get; set; }

    private int animeIndex;
    public int AnimeIndex
    {
        get => animeIndex;
        set
        {
            if (value > animeSprites.Length - 1)
                animeIndex = 0;
            else if (value < 0)
                animeIndex = 0;
            else
                animeIndex = value;
        }
    }

    public EnemyComponent(Sprite[] sprites, float scale)
    {
        collisionComponent = new();
        animeSprites = sprites;
        spriteScale = scale;
    }

    public void Update()
    {
        collisionComponent.X = X;
        collisionComponent.Y = Y;
        collisionComponent.Width = animeSprites[AnimeIndex].Width * spriteScale;
        collisionComponent.Width = animeSprites[AnimeIndex].Height * spriteScale;
    }

    public void Render()
    {
        if (!IsDead)
            return;

        animeSprites[AnimeIndex].HorizontalScale = spriteScale;
        animeSprites[AnimeIndex].VerticalScale = spriteScale;
        animeSprites[AnimeIndex].Render(X, Y);
    }
}