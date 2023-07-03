namespace SpaceInvaders.Frame;

internal class SceneElement
{
    public readonly Scene Owner;

    public SceneElement(Scene owner)
        => Owner = owner;

    public virtual void Init()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Render()
    {
    }

    public virtual void Finish()
    {
    }
}