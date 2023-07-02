using SpaceInvaders.App;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class Player
{
    private Sprite playerSprite;

    public readonly CollisionComponent Collision;
    public float X { get; set; }
    public float Y { get; set; }

    public Player(Sprite player)
    {
        playerSprite = player;
        Collision = new();
    }

    public void Init()
    {
        X = (AppInfo.Width - playerSprite.ActualWidth) / 2;
        Y = (AppInfo.Height / 100) * 85;
    }

    public void Update()
    {
        Collision.X = X;
        Collision.Y = Y;
        Collision.Width = playerSprite.ActualWidth;
        Collision.Height = playerSprite.ActualHeight;
    }

    public void Render()
    {
        playerSprite?.Render(X, Y);
    }
}