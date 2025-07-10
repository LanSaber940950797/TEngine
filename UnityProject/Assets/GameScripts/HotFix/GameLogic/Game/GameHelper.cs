using ET;
using GameLogic.Battle;


namespace GameLogic
{
    public static class GameHelper
    {
        public static GameWorld WorldScene => GameModule.ECS.Root.GetComponent<GameWorld>();
        public static Scene CurrentScene => GameModule.ECS.Root.GetComponent<CurrentScenesComponent>().Scene;
        public static Scene Battle => GameModule.ECS.Root.GetComponent<CurrentScenesComponent>().Scene;
       
        
        
        
        public static async ETTask StartGame()
        {
            var world = GameModule.ECS.Root.AddComponent<GameWorld>();
            
            await ETTask.CompletedTask;
        }
        
        
        
        public static void InitBattle(Scene battle)
        {
            //逻辑层组件
            battle.AddComponent<BattleTime>();
            battle.AddComponent<BattleTimerComponent>();
            battle.AddComponent<ActorComponent>();
            
            //显示层组件
            battle.AddComponent<ActorViewComponent>();
            battle.AddComponent<BattleRootView>();
        }
       

    
    }
}