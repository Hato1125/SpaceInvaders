namespace SpaceInvaders.Frame;

internal class Scene
{
    protected readonly List<Scene> Children = new();
    public SceneState State = SceneState.Active;

    public virtual void Init()
    {
        foreach (var child in Children)
            child.Init();
    }

    public virtual void Update()
    {
        foreach (var child in Children)
        {
            if (child.State == SceneState.Active)
                child.Update();
        }
    }

    public virtual void Render()
    {
        foreach (var child in Children)
            child.Render();
    }

    public virtual void Finish()
    {
        foreach (var child in Children)
            child.Finish();
    }
}

public enum SceneState
{
    Active,
    Inactive,
}