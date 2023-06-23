using SpaceInvaders.App;
using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class Player
{
    private Sprite? playerSprite;

    public readonly CollisionComponent Collision;
    public float X { get; set; }
    public float Y { get; set; }

    public Player()
    {
        Collision = new();
    }

    public void Init(Sprite player)
    {
        playerSprite = player;
        X = (AppInfo.Width - player.ActualWidth) / 2;
        Y = (AppInfo.Height / 100) * 85;
    }

    public void Update()
    {
        if(playerSprite == null)
            return;

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