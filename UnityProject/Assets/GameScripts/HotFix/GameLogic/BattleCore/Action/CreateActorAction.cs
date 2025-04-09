using ET;

namespace GameLogic.Battle
{
    [ChildOf(typeof(ActionComponent))]
    public class CreateActorAction : Entity, IAwake,IAction,IDestroy
    {
        public ActorType ActorType;
        public SideType SideType;
        public Entity Info;
        public Actor Creator { get; set; }
        public Actor Target { get; set; }
        public IAction SourceAction { get; set; }
        
    }
    
    [EntitySystemOf(typeof(CreateActorAction))]
    public static partial class CreateActorActionSystem
    {
        [EntitySystem]
        private static void Awake(this CreateActorAction self)
        {
            
        }

        [EntitySystem]
        private static void Destroy(this CreateActorAction self)
        {
            self.Creator = null;
            self.Target = null;
            self.Info = null;
            self.SourceAction = null;
        }
        
        public static void DoAction(this CreateActorAction self)
        {
            self.PreProcess();
            self.DoActionInner();
            self.PostProcess();
        }
        
        private static void PreProcess(this CreateActorAction self)
        {
            
        }
        private static void PostProcess(this CreateActorAction self)
        {
           self.Scene().SendEvent(BattleEvent.InitActor, self);
           self.Scene().SendEvent(BattleEvent.InitActorView, self);
           self.Scene().SendEvent(BattleEvent.ActorCreate, self);
           //延时销毁，可能有些事件监听需要延时处理该行为，不能立即销毁
           self.AddChild<DestroyTimer, int, bool>(1000, true, true);
        }
        
        private static void DoActionInner(this CreateActorAction self)
        {
            var actorComponent =  self.Scene().GetComponent<ActorComponent>();
            self.Target = actorComponent.AddChild<Actor, ActorType, SideType>(self.ActorType, self.SideType);
            if (self.Info != null)
            {
                self.Target.AddComponent(self.Info);
            }
        }
    }
    
    
}