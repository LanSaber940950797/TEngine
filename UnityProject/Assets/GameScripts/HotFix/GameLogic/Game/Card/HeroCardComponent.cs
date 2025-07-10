using System.Collections.Generic;
using System.Linq;
using ET;

namespace GameLogic
{
    [ComponentOf(typeof(CardsComponent))]
    public class HeroCardComponent : Entity,IAwake
    {
        public static int MaxCount = 1;
        public int Idx = 0;
    }
    
    [EntitySystemOf(typeof(HeroCardComponent))]
    public static partial class HeroCardComponentSystem
    {
        public static bool AddHeroCard(this HeroCardComponent self, Card card)
        {
            if (self.IsFull())
            {
                return false;
            }

            for (int i = 1; i <= self.MaxCardCount(); i++)
            {
                self.Idx++;
                if (self.Idx > GameHelper.HeroLineCount())
                {
                    self.Idx = 1;
                }
                var actor = GameHelper.LsWorld.GetComponent<CardActorComponent>().GetActorByPos(self.Idx);
                if (actor == null || actor.IsDead)
                {
                    continue;
                }
                var heroCard = actor.GetComponent<HeroCard>();
                heroCard.AddCard(card);
                self.AddChild(card);
                return true;
            }
            

            
            return false;
        }

        public static bool IsFull(this HeroCardComponent self)
        {
            return self.Children.Count >= self.MaxCardCount();
        }

        public static int MaxCardCount(this HeroCardComponent self)
        {
            return CardGameHelper.ActiveHeroCount();
        }

        public static void ToDiscard(this HeroCardComponent self)
        {
            var list = self.Children.Values.ToList();
            foreach (Card card in list)
            { 
                HeroCard heroCard = card.HeroCard;
                if (heroCard != null)
                {
                    heroCard.Clear();
                }
                self.Parent.GetComponent<DiscardCardComponent>().AddChild(card);
            }

            self.Idx = 0;
        }
        
        public static void ToHandCard(this HeroCardComponent self)
        {
            var list = self.Children.Values.ToList();
            foreach (Card card in list)
            { 
                HeroCard heroCard = card.HeroCard;
                if (heroCard != null)
                {
                    heroCard.Clear();
                }
                self.Parent.GetComponent<HandCardComponent>().AddChild(card);
            }
            self.Idx = 0;
        }
        
        public static Dictionary<CardType,  List<Card>> GetCardTypes(this HeroCardComponent self)
        {
            Dictionary<CardType, List<Card>> dict = new Dictionary<CardType,  List<Card>>();
            foreach (Card card in self.Children.Values)
            {
                if (dict.TryGetValue(card.CardType, out var list))
                {
                    list.Add(card);
                }
                else
                {
                    list = new List<Card>();
                    list.Add(card);
                    dict.Add(card.CardType, list);
                }
            }
            return dict;
        }
        
        public static void ToCardPile(this HeroCardComponent self)
        {
            var list = self.Children.Values.ToList();
            var cardComp = self.GetParent<CardsComponent>();
            foreach (Card card in list)
            {
                HeroCard heroCard = card.HeroCard;
                if (heroCard != null)
                {
                    heroCard.Clear();
                }
                
                cardComp.AddChild(card);
                cardComp.AddCardPile(card);
            }
            self.Idx = 0;
        }
        
        
    }
}