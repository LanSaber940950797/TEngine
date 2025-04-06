using System;
using ET;
using GameConfig;
using GameConfig.Battle;

namespace GameLogic.Battle
{
    [ChildOf(typeof(AbilityEffectComponent))]
    public class AbilityEffect : Entity, IAwake<EffectDesc>, IDestroy
    {
        public EffectDesc Desc;
        public Actor Owner => (Parent.Parent as IAbility).Owner;
    }
    
    [EntitySystemOf(typeof(AbilityEffect))]
    public static partial class AbilityEffectSystem
    {
        [EntitySystem]
        private static void Awake(this AbilityEffect self, EffectDesc desc)
        {
            self.Desc = desc;
            self.AddEffectComponent(desc.Effect.EffectType);
            self.InitEffectTrigger();
        }

        private static void InitEffectTrigger(this AbilityEffect self)
        {
            var triggerDesc = self.Desc.Trigger;
            switch (triggerDesc.Type)
            {
                case EffectTriggerType.Create:
                    var action = self.CreateAssignAction(self.Owner);
                    action.DoAction().NoContext();
                    break;
                case EffectTriggerType.Spell:
                    break;
                case EffectTriggerType.ActionEvent:
                    self.AddComponent<ActionEventTrigger>();
                    break;
                default:
                    self.Scene().SendEvent(BattleEvent.InitEffectTrigger, self);
                    break;
            }
        }


        
        private static void AddEffectComponent(this AbilityEffect self, EffectType effectType)
        {
            Type type = AbilityEffectSingleton.Instance.GetEffectType(effectType);
            var component = self.AddComponent(type);
            self.AddEventListener(BattleEvent.DoEffect, component);
        }
        
        [EntitySystem]
        private static void Destroy(this AbilityEffect self)
        {
            self.Desc = null;
        }
        
        
        
        public static EffectAssignAction CreateAssignAction(this AbilityEffect self, Actor target, IAction source = null)
        {
            if (self.Owner.TryMakeAction<EffectAssignAction>(out var action, source))
            {
                action.AssignTarget = target;
                action.AbilityEffect = self;
            }
            return action;
        }
    }
}