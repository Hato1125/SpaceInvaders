namespace SpaceInvaders.Frame;

internal static class SceneManager
{
    private static readonly Dictionary<string, Scene> scenes = new();

    public static string CurrentSceneName { get; private set; } = string.Empty;

    public static void AddScene(string name, Scene scene)
    {
        scenes.Add(name, scene);
    }

    public static void RemoveScene(string name)
    {
        if (!scenes.ContainsKey(name))
            return;

        if (CurrentSceneName == name)
        {
            scenes[CurrentSceneName].Finish();
            CurrentSceneName = string.Empty;
        }

        scenes.Remove(name);
    }

    public static void ChangeScene(string name)
    {
        if (!scenes.ContainsKey(name) || CurrentSceneName == name)
            return;

        if (CurrentSceneName != string.Empty)
            scenes[CurrentSceneName].Finish();

        CurrentSceneName = name;
        scenes[CurrentSceneName].Init();
    }

    public static void SceneUpdate()
    {
        if (CurrentSceneName == string.Empty)
            return;

        scenes[CurrentSceneName].Update();
        scenes[CurrentSceneName].Render();
    }

    public static void RemoveAllScene()
    {
        scenes[CurrentSceneName].Finish();
        scenes.Clear();
    }
}