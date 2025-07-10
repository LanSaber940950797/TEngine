using System.Linq;
using ET;

namespace GameLogic
{
    //手牌组件
    [ComponentOf(typeof(HandCardComponent))]
    public class HandCardComponent : Entity,IAwake
    {
        
    }
    
    [EntitySystemOf(typeof(HandCardComponent))]
    public static partial class HandCardComponentSystem
    {
        public static void ToCardPile(this HandCardComponent self)
        {
            var list = self.Children.Values.ToList();
            var cardComp = self.GetParent<CardsComponent>();
            foreach (Card card in list)
            {
                cardComp.AddChild(card);
                cardComp.AddCardPile(card);
            }
        }
    }
}