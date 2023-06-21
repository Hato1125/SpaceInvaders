﻿using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class EnemyComponent
{
    public readonly CollisionComponent CollisionComponent;
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
        CollisionComponent = new();
        animeSprites = sprites;
        spriteScale = scale;
        IsDead = false;
        IsCollision = true;
    }

    public void Update()
    {
        if (IsDead)
            return;

        CollisionComponent.X = X;
        CollisionComponent.Y = Y;
        CollisionComponent.Width = animeSprites[AnimeIndex].Width * spriteScale;
        CollisionComponent.Height = animeSprites[AnimeIndex].Height * spriteScale;
    }

    public void Render()
    {
        if (IsDead)
            return;

        animeSprites[AnimeIndex].HorizontalScale = spriteScale;
        animeSprites[AnimeIndex].VerticalScale = spriteScale;
        animeSprites[AnimeIndex].Render(X, Y);
    }
}