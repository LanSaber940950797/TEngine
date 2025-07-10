using System.Collections.Generic;
using ET;

namespace GameLogic
{
    /// <summary>
    /// 牌型羁绊组件
    /// </summary>
    
    [ComponentOf]
    public class CardFetterComponent : Entity, IAwake
    {
        
    }
    
    [EntitySystemOf(typeof(CardFetterComponent))]
    public static partial class CardFetterComponentSystem
    {
        [EntitySystem]
        public static void Awake(this CardFetterComponent self)
        {
            for (CardType type = CardType.Fire; type < CardType.Max; type++)
            {
                self.AddChild<CardFetter, CardType, int>(type, 1);
            }
        }

        public static void CalcCardFetter(this CardFetterComponent self)
        {
            var heroCardComponent = GameHelper.WorldScene.GetComponent<CardsComponent>().GetComponent<HeroCardComponent>();
            var dict = heroCardComponent.GetCardTypes();
            foreach (CardFetter fetter in self.Children.Values)
            {
                if (dict.TryGetValue(fetter.Type, out var list))
                {
                    fetter.CalcCardFetter(list);
                }
                
            }
        }

       
    }
}