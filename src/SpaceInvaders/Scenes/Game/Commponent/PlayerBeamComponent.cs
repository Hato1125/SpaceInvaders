using SpaceInvaders.Graphics;

namespace SpaceInvaders.Scenes.Game;

internal class PlayerBeamComponent
{
    private const float BEAM_SPEED = 550;
    private readonly Sprite beamSprite;
    private readonly CollisionComponent collision;

    private float beamX;
    private float beamY;

    public PlayerBeamComponent(Sprite beam, float beginX, float beginY)
    {
        beamSprite = beam;
        beamX = beginX;
        beamY = beginY;

        collision = new();
        BeamScreen.PlayerBeam = this;
    }

    public void Update()
    {
        beamY -= (float)(BEAM_SPEED * App.App.Window.DeltaTime);

        if (beamY < -beamSprite.Height)
            BeamScreen.PlayerBeam = null;

        collision.X = beamX;
        collision.Y = beamY;
        collision.Width = beamSprite.ActualWidth;
        collision.Height = beamSprite.ActualHeight;
    }

    public void Render()
    {
        beamSprite.Render(beamX, beamY);
    }
}