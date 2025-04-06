using ET;
using MemoryPack;



namespace GameLogic.Battle
{
    [ChildOf(typeof(ActorComponent))]
    public partial class Actor : Entity,IAwake<ActorType, SideType>,IDestroy
    {
        public ActorType ActorType;
       
        public SideType SideType;
        
        private EntityRef<ActorView> actorView;

        public ActorView ActorView
        {
            get
            {
                return actorView;
            }
            set
            {
                actorView = value;
            }
        }
        
    }
    
    [EntitySystemOf(typeof(Actor))]
    public static partial class ActorSystem
    {
        [EntitySystem]
        private static void Awake(this Actor self, ActorType actorType, SideType sideType)
        {
            self.ActorType = actorType;
            self.SideType = sideType;

            self.AddComponent<ActionComponent>();
            self.AddComponent<SkillComponent>();
            self.AddComponent<BuffComponent>();
            self.AddComponent<TransformComponent>();
            self.AddComponent<StatusComponent>();
        }

        [EntitySystem]
        private static void Destroy(this Actor self)
        {
            self.ActorView = null;
        }
        
        public static bool IsActorType(this Actor self, ActorType type)
        {
            return (self.ActorType & type) > 0;
        }
    }
}