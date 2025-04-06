using ET;

namespace GameLogic.Battle
{
    [ComponentOf(typeof(Actor))]
    public class BuffComponent : Entity, IAwake
    {
        
    }
    
    [EntitySystemOf(typeof(BuffComponent))]
    public static partial class BuffComponentSystem
    {
        [EntitySystem]
        public static void Awake(this BuffComponent self)
        {
            
        }

        public static Buff GetBuff(this BuffComponent self, int buffId)
        {
            return self.GetChild<Buff>(buffId);
        }
    }
}