namespace SpaceInvaders.Frame;

internal class Scene
{
    protected readonly List<Scene> Children = new();
    protected readonly List<SceneElement> Elements = new();
    public SceneState State = SceneState.Active;

    public virtual void Init()
    {
        foreach (var child in Children)
            child.Init();

        foreach (var element in Elements)
            element.Init();
    }

    public virtual void Update()
    {
        foreach (var child in Children)
        {
            if (child.State == SceneState.Active)
                child.Update();
        }

        if (State == SceneState.Active)
        {
            foreach (var element in Elements)
                element.Update();
        }
    }

    public virtual void Render()
    {
        foreach (var child in Children)
            child.Render();

        foreach (var element in Elements)
            element.Render();
    }

    public virtual void Finish()
    {
        foreach (var child in Children)
            child.Finish();

        foreach (var element in Elements)
            element.Finish();
    }
}

public enum SceneState
{
    Active,
    Inactive,
}