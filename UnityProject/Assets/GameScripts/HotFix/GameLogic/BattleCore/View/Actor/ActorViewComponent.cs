using ET;

namespace GameLogic.Battle
{
    [ComponentOf]
    public class ActorViewComponent : Entity, IAwake, IDestroy
    {
        
    }
    
    [EntitySystemOf(typeof(ActorViewComponent))]
    public static partial class ActorViewComponentSystem
    {
        [EntitySystem]
        public static void Awake(this ActorViewComponent self)
        {
            self.Scene().AddEventListener(BattleEvent.InitActor, self);
            
        }
        
        [EntitySystem]
        public static void Destroy(this ActorViewComponent self)
        {
            self.Scene().RemoveEventListener(BattleEvent.InitActor, self);
        }

        [EventDispatcher(BattleEvent.InitActor)]
        public static async ETTask OnActorCreate(this ActorViewComponent self, CreateActorAction action)
        {
            var actor = action.Target;
            self.AddChildWithId<ActorView, Actor>(actor.Id, actor);
            await ETTask.CompletedTask;
        }
        
    }
}