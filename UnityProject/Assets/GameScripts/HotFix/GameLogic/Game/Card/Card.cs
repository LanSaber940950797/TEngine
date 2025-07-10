using System.Drawing;
using ET;
using GameLogic.Battle;

namespace GameLogic
{
    public enum CardType
    {
        None,
        //红
        // Red = 1,
        // //黄
        // Yellow = 2,
        // //蓝
        // Blue = 3,
        // //绿
        // Green = 4,
        //
        // Max = 5,
        Fire = 1,
        Water = 2,
        Wind = 3,
        Thunder = 4,
        Earth = 5,
        Max = 6,
    }
    
    [ChildOf]
    public class Card : Entity, IAwake<CardType, int>
    {
        
        public  CardType CardType;
        
        public int Point;
        public EntityRef<HeroCard> HeroCard;
    }

    [EntitySystemOf(typeof(Card))]
    public static partial class CardSystem
    {
        [EntitySystem]
        public static void Awake(this Card self, CardType cardType, int point)
        {
         
            self.CardType = cardType;
            self.Point = point;
        }
    }
}