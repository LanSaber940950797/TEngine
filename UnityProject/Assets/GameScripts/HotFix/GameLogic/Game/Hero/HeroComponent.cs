using ET;
using GameLogic.Battle;

namespace GameLogic
{
    [ComponentOf]
    public class HeroComponent : Entity,IAwake
    {
        
    }

    [EntitySystemOf(typeof(HeroComponent))]
    public static partial class HeroComponentSystem
    {
        [EntitySystem]
        public static void Awake(this HeroComponent self)
        {
            self.InitHero();
        }
        
        private static void InitHero(this HeroComponent self)
        {
            //self.AddChild<Hero, ActorType, int, int>(ActorType.Player, 1, 0);
            for (int i = 1; i <= 5; i++)
            {
                self.AddChild<Hero, ActorType, int, int>(ActorType.Hero, 100 + i, i);
            }
        }
        
        
    }
}