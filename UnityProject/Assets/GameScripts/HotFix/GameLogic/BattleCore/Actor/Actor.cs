using ET;
using MemoryPack;



namespace GameLogic.Battle
{
    [MemoryPackable]
    [ChildOf(typeof(ActorComponent))]
    public partial class Actor : Entity,IAwake
    {
        public ActorType ActorType;
       
        public SideType SideType;
       
        public int DescId;
    }
}