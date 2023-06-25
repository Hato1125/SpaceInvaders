using SpaceInvaders.Frame;

namespace SpaceInvaders.Scenes.Game;

internal class GameClear : Scene
{
    private bool isGameClear;
    public bool IsGameClear
    {
        get => isGameClear;
        set
        {
            isGameClear = value;
            var sceneState = value
                ? SceneState.Inactive
                : SceneState.Active;

            var waitScene = new Scene[] {
                GameScene.BeamScreen,
                GameScene.Player,
            };

            SceneManager.WaitScene(waitScene, 2);
        }
    }

    public override void Update()
    {
        if (!isGameClear)
            return;
    }

    public override void Render()
    {
        if (!isGameClear)
            return;
    }
}