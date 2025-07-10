using System.Linq;
using ET;

namespace GameLogic
{
    /// <summary>
    /// 弃牌组件
    /// </summary>
    [ComponentOf(typeof(CardsComponent))]
    public class DiscardCardComponent : Entity, IAwake
    {
        
    }
    
    [EntitySystemOf(typeof(DiscardCardComponent))]
    public static partial class DiscardCardComponentSystem
    {
        public static void ToCardPile(this DiscardCardComponent self)
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