using System.Collections.Generic;
using System.Linq;
using ET;


namespace GameLogic
{
    
    [ComponentOf(typeof(Scene))]
    public class CardsComponent : Entity,IAwake
    {
        
        //牌堆
        public List<EntityRef<Card>> CardPile = new List<EntityRef<Card>>();
       
    }

    [EntitySystemOf(typeof(CardsComponent))]
    public static partial class CardsComponentSystem 
    {
        [EntitySystem]
        public static void Awake(this CardsComponent self)
        {
            self.AddComponent<HandCardComponent>();
            self.AddComponent<DiscardCardComponent>();
            self.AddComponent<HeroCardComponent>();
            self.InitCard();
        }
        
        
        public static void AddCardPile(this CardsComponent self, Card card)
        {
            self.CardPile.Add(card);
        }
        
 
        //抽取
        public static List<EntityRef<Card>> DrawCards(this CardsComponent self, int count)
        {
            List<EntityRef<Card>> cards = new List<EntityRef<Card>>();
            while (count > 0 && self.CardPile.Count > 0)
            {
                cards.Add(self.CardPile.LastOrDefault());
                self.CardPile.RemoveAt(self.CardPile.Count - 1);
                count--;
            }
            return cards;
        }
        
        //洗牌
        public static void Shuffle(this CardsComponent self)
        {
            int n = self.CardPile.Count;
            while (n > 1)
            {
                n--;
                int k = self.Random.Next(0, n + 1);
                EntityRef<Card> value = self.CardPile[k];
                self.CardPile[k] = self.CardPile[n];
                self.CardPile[n] = value;
            }
        }

        public static void StartBattle(this CardsComponent self)
        {
            self.ToCardPile();
            self.Shuffle();
        }
        
        public static void ToCardPile(this CardsComponent self)
        {
            self.GetComponent<HandCardComponent>().ToCardPile();
            self.GetComponent<DiscardCardComponent>().ToCardPile();
            self.GetComponent<HeroCardComponent>().ToCardPile();
        }
        
        private static void InitCard(this CardsComponent self)
        {
            for (int i = 1; i < 6; i++)
            {
                for (CardType type = CardType.Fire; type < CardType.Max; type++)
                {
                    Card card = self.AddChild<Card, CardType, int>(type, i);
                    self.AddCardPile(card);
                }
            }
        }
        
        //最大出牌数
        public static int MaxCardCount(this CardsComponent self)
        {
            return 5;
        }
        
    }
}