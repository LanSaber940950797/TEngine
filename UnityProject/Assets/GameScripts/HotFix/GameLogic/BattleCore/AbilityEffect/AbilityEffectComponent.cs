using System.Collections.Generic;
using ET;
using GameConfig;


namespace GameLogic.Battle
{
    [ComponentOf]
    public class AbilityEffectComponent : Entity,IAwake<int, int>
    {
        
        public int EffectId;
        public int EffectLevel;
    }
    
    [EntitySystemOf(typeof(AbilityEffectComponent))]
    public static partial class AbilityEffectComponentSystem
    {
        [EntitySystem]
        private static void Awake(this AbilityEffectComponent self, int id, int level)
        {
            self.EffectId = id;
            self.EffectLevel = level;
            var effects = TbEffect.Instance.Get2Key(id, level);
            foreach (var desc in effects.Values)
            {
                var effect = self.AddChildWithId<AbilityEffect, EffectDesc>(desc.Index, desc, true);
            }
           
        }
        
        public static EffectAssignAction CreateAssignAction(this AbilityEffectComponent self, Actor target, int index)
        {
            var abilityEffect = self.GetChild<AbilityEffect>(index);
            var effectAssign = abilityEffect.CreateAssignAction(target);
            return effectAssign;
        }
        
        public static List<EffectAssignAction> CreateAssignActions(this AbilityEffectComponent self, Actor target)
        {
            List<EffectAssignAction> list = new List<EffectAssignAction>();
            foreach (AbilityEffect abilityEffect in self.Children.Values)
            {
                var effectAssign = abilityEffect.CreateAssignAction(target);
                list.Add(effectAssign);
            }
            return  list;
        }
    }
}