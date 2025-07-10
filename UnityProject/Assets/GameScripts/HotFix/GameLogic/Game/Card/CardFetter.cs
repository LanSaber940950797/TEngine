using System.Collections.Generic;
using ET;
using GameConfig;

namespace GameLogic
{
    public enum CardFetterType
    {
        None = 0,
        Pairs = 1, //两种同颜色
        TwoPairs = 2, //两对同颜色
        ThreeTwo = 3, //三张同颜色二张
        AllSame = 4,//同色
    }
    public class CardFetter : Entity, IAwake<CardType, int>
    {
        public CardType Type;
        //羁绊等级
        public int Level;
        public int AddValue;
    }

    [EntitySystemOf(typeof(CardFetter))]
    public static partial class CardFetterSystem
    {
        [EntitySystem]
        public static void Awake(this CardFetter self, CardType type, int level)
        {
            self.Type = type;
            self.Level = level;
        }
        
        public static void CalcCardFetter(this CardFetter self, List<Card> cards)
        {
            if (cards.Count <= 0)
            {
                return;
            }

            var desc = TbFetterDesc.Instance.Get(self.Level);
            int addRate = desc.RateAdds[cards.Count - 1];
            foreach (var card in cards)
            {
                HeroCard heroCard = card.HeroCard;
                heroCard.AddProp(NumericType.AttackFinalAdd, addRate);
            }
        }
    }
}