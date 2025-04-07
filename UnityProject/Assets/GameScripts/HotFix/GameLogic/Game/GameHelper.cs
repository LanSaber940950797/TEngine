using ET;
using GameLogic.Battle;


namespace GameLogic
{
    public static class GameHelper
    {
        //public static Scene WorldScene => GameModule.ECS.Root.GetComponent<CurrentScenesComponent>().Scene;
        public static Scene CurrentScene => GameModule.ECS.Root.GetComponent<CurrentScenesComponent>().Scene;
        
       
        
        
        
        public static async ETTask StartGame()
        {
            var battle = CurrentSceneFactory.Create(IdGenerater.Instance.GenerateId(),IdGenerater.Instance.GenerateInstanceId()
                , SceneType.Battle,"战斗");
            InitBattle(battle);
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