using System;
using System.Collections.Generic;
using System.Reflection;
using ET;
using GameConfig.Battle;

namespace GameLogic.Battle
{
    [Code]
    public class AbilityEffectSingleton :Singleton<AbilityEffectSingleton>, ISingletonAwake
    {
        public Dictionary<EffectType, Type> EffectDic = new Dictionary<EffectType, Type>();
        public void Awake()
        {
            EffectDic.Clear();
            foreach (Type type in CodeTypes.Instance.GetTypes(typeof(EAbilityEffectAttribute)))
            {
                EAbilityEffectAttribute eAbilityEffectAttribute = type.GetCustomAttribute<EAbilityEffectAttribute>();
                EffectDic.Add(eAbilityEffectAttribute.Type, type);
            }
        }
        
        public Type GetEffectType(EffectType type)
        {
            return EffectDic[type];
        }
    }
}