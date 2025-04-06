using ET;

namespace GameLogic.Battle
{
    
    public class BattleScene : Entity, IScene,IAwake
    {
        public Fiber Fiber { get; set; }
        public SceneType SceneType { get; set; }
    }
    
    [EntitySystemOf(typeof(BattleScene))]
    public static partial class BattleSceneSystem
    {
        [EntitySystem]
        private static void Awake(this BattleScene self)
        {
            self.SceneType = SceneType.Battle;
        }

        public static void Init(this BattleScene self)
        {
            //逻辑层组件
            self.AddComponent<ActorComponent>();
            
            //显示层组件
            self.AddComponent<ActorViewComponent>();
            self.AddComponent<BattleRootView>();
        }
    }
}