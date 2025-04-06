namespace ET
{
    public struct AfterCreateCurrentScene
    {
    }
    
    public static class CurrentSceneFactory
    {
        public static Scene Create(long id, long instanceId, SceneType sceneType, string name)
        {
            var currentScenesComponent = GameModule.ECS.Root.GetComponent<CurrentScenesComponent>();
            Scene currentScene = EntitySceneFactory.CreateScene(currentScenesComponent, id, instanceId, sceneType, name);
            if (currentScenesComponent.Scene != null)
            {
                currentScenesComponent.Scene.Dispose();
            }
            currentScenesComponent.Scene = currentScene;
            EventSystem.Instance.Publish(currentScene, new AfterCreateCurrentScene());
            return currentScene;
        }
    }
}