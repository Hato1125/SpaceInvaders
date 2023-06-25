using System.Diagnostics;

namespace SpaceInvaders.Frame;

internal static class SceneManager
{
    private static readonly Dictionary<string, Scene> scenes = new();
    private static readonly Stopwatch waitStopwatch = new();
    private static Scene[]? waitScenes;
    private static int waitTimeMs = 0;

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

        if (waitStopwatch.IsRunning && waitScenes != null)
        {
            Console.WriteLine(waitStopwatch.Elapsed.TotalSeconds + " >" + waitTimeMs);
            if (waitStopwatch.Elapsed.TotalSeconds > waitTimeMs)
            {
                foreach (var scene in waitScenes)
                    scene.State = SceneState.Active;

                waitTimeMs = 0;
                waitScenes = null;
                waitStopwatch.Stop();
                waitStopwatch.Reset();
            }
        }

        if (scenes[CurrentSceneName].State == SceneState.Active)
            scenes[CurrentSceneName].Update();

        scenes[CurrentSceneName].Render();
    }

    public static void RemoveAllScene()
    {
        scenes[CurrentSceneName].Finish();
        scenes.Clear();
    }

    public static void WaitScene(Scene[] scenes, int ms)
    {
        if (waitStopwatch.IsRunning)
            return;

        foreach (var scene in scenes)
            scene.State = SceneState.Inactive;

        waitStopwatch.Start();
        waitScenes = scenes;
        waitTimeMs = ms;
    }

    public static Scene GetScene(string name)
        => scenes[name];
}