using ET;

namespace GameLogic.Battle
{
    public static class BattleHelper
    {
        public static int ToFrameTime(float time)
        {
            return (int)(time * 1000);
        }
        public static Scene Scene => GameHelper.CurrentScene.SceneType == SceneType.Battle ? GameHelper.CurrentScene : null;

        public static bool TryMakeAction<T>(out T action, IAction source = null) where T : Entity, IAction,IAwake
        {
            action = null;
            var system = BattleHelper.Scene.GetComponent<ActorComponent>().SystemActor;
            if (system == null)
            {
                return false;
            }

            return system.TryMakeAction(out action, source);
        }

        public static long FrameTime()
        {
            return Scene.GetComponent<BattleTime>().Now();
        }
        
        public static Actor CreateActor(ActorType actorType, SideType sideType, Entity info = null)
        {
            if (TryMakeAction<CreateActorAction>(out var action))
            {
                action.ActorType = actorType;
                action.SideType = sideType;
                action.Info = info;
                action.DoAction();
                return action.Target;
            }

            return null;
        }
    }
}