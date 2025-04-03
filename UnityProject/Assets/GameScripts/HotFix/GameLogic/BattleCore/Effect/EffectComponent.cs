using ET;
using GameConfig;

namespace GameLogic.Battle
{
    [ComponentOf]
    public class EffectComponent : Entity,IAwake<int, int>
    {
        public EffectDesc Desc;
    }
    
    [EntitySystemOf(typeof(EffectComponent))]
    public static partial class EffectComponentSystem
    {
        [EntitySystem]
        private static void Awake(this EffectComponent self, int id, int level)
        {
            self.Desc = TbEffect.Instance.Get(id, level);
            if (self.Desc == null)
            {
                Log.Error($"EffectComponentSystem.Awake no desc {id} {level}");
                return;
            }

            for (int i = 0; i < self.Desc.Effects.Count; i++)
            {
                var effectDesc = self.Desc.Effects[i];
                self.AddChildWithId<Effect, GameConfig.Battle.Effect>(i + 1, effectDesc, true);
            }
        }
    }
}