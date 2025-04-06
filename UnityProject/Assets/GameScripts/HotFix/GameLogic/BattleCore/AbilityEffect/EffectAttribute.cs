using System;
using ET;
using GameConfig.Battle;

namespace GameLogic.Battle
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EAbilityEffectAttribute : BaseAttribute
    {
        public EffectType Type;
        public EAbilityEffectAttribute(EffectType type) => Type = type;
    }
}