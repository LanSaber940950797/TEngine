using ET;

namespace GameLogic
{
    [ComponentOf]
    public class GameWorld : Entity,IScene,IAwake
    {
        public Fiber Fiber { get; set; }
        public SceneType SceneType { get; set; }
    }

    [EntitySystemOf(typeof(GameWorld))]
    public static class GameWorldSystem
    {
        public static void Awake(this GameWorld self)
        {
            
        }
        
    }
}