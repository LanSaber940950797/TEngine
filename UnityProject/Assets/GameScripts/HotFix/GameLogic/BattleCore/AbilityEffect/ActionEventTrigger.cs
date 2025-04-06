using ET;
using GameConfig.Battle;

namespace GameLogic.Battle
{
    [ComponentOf(typeof(AbilityEffect))]
    public class ActionEventTrigger : Entity,IAwake,IDestroy
    {
       
    }
    
    [EntitySystemOf(typeof(ActionEventTrigger))]
    public static partial class ActionEventTriggerSystem
    {
        [EntitySystem]
        private static void Awake(this ActionEventTrigger self)
        {
            var effect = self.GetParent<AbilityEffect>();
            var desc = effect.Desc.Trigger;
            effect.Owner.AddEventListener(desc.Param1, self);
        }

        [EntitySystem]
        private static void Destroy(this ActionEventTrigger self)
        {
            var effect = self.GetParent<AbilityEffect>();
            var desc = effect.Desc.Trigger;
            effect.Owner.RemoveEventListener(desc.Param1, self);
        }
        [EventDispatcher(BattleEvent.AllEvent)]
        private static async ETTask OnEvent(this ActionEventTrigger self, int eventId, params object[] parmaList)
        {
            if (parmaList.Length < 1)
            {
                return;
            }

            if (!(parmaList[0] is IAction triggerAction))
            {
                return;
            }
            
            var effect = self.GetParent<AbilityEffect>();
            var action = effect.CreateAssignAction(null, triggerAction);
            action.DoAction().NoContext();
            await ETTask.CompletedTask;
        }
    }
    
}