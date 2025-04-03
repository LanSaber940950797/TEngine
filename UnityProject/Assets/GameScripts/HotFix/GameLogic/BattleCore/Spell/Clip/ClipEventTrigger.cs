using ET;

namespace GameLogic.Battle
{
    [ChildOf]
    public class ClipEventTrigger : Entity, IAwake<SpellClipData>, IDestroy
    {
        public SpellClipData Data;
    }
    
    [EntitySystemOf(typeof(ClipEventTrigger))]
    public static partial class ClipEventTriggerSystem
    {
        [EntitySystem]
        public static void Awake(this ClipEventTrigger self, SpellClipData data)
        {
            self.Data = data;
            self.Parent.AddEventListener(self.Data.Event, self);
        }

        [EventDispatcher(BattleEvent.AllEvent)]
        public static async ETTask OnEvent(this ClipEventTrigger self, params object[] args)
        {
            self.Data.AttachClip(self.Parent);
            await ETTask.CompletedTask;
        }
        
        [EntitySystem]
        public static void Destroy(this ClipEventTrigger self)
        {
            self.Parent.RemoveEventListener(self.Data.Event, self);
            self.Data = null;
        }
    }
}