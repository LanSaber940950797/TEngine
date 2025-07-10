using ET;
using GameConfig;
using GameLogic.Battle;

namespace GameLogic
{
    [ChildOf(typeof(HeroComponent))]
    public class Hero : Entity, IAwake<ActorType, int, int>
    {
        public ActorType Type;
        public int DescId;
        public int Pos;
        public HeroDesc Desc;
    }

    [EntitySystemOf(typeof(Hero))]
    public static partial class HeroSystem
    {
        [EntitySystem]
        public static void Awake(this Hero self, ActorType type, int descId, int pos)
        {
            self.Type = type;
            self.DescId = descId;
            self.Pos = pos;
            self.Desc = TbHeroDesc.Instance.Get(self.DescId);
        }

        public static int GetHp(this Hero self)
        {
            return self.Desc.Hp;
        }
        
        public static int GetAttack(this Hero self)
        {
            return self.Desc.Attack;
        }
    }
}