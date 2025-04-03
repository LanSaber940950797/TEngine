using ET;

namespace GameLogic.Battle
{
    [ChildOf(typeof(EffectComponent))]
    public class Effect : Entity, IAwake<GameConfig.Battle.Effect>, IDestroy
    {
        public GameConfig.Battle.Effect Desc;
    }
    
    [EntitySystemOf(typeof(Effect))]
    public static partial class EffectSystem
    {
        [EntitySystem]
        private static void Awake(this Effect self, GameConfig.Battle.Effect desc)
        {
            self.Desc = desc;
        }

        [EntitySystem]
        private static void Destroy(this Effect self)
        {
            self.Desc = null;
        }

        public static async ETTask DoEffect(this Effect self)
        {
            await ETTask.CompletedTask;
        }
    }
}