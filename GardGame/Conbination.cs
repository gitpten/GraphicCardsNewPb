using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardGame
{
    public class Combination
    {
        enum Comb
        {
            Pair
        }

        Card OlderCard { get; set; }

        public int CountOfSimilarCard(CardSet cards)
        {
            cards.Sort();
             int max = 1;

            foreach (var card in cards.Cards)
            {
                int co = cards.Cards.Count(c => c.Figure == card.Figure);
                if (co > max)
                    max = co;
            }
            return max;
        }

        public bool IsRolyalFlash(CardSet card)
        {
            card.Sort();
            if (!StreetFlash(card)) return false;

            return card[0].Figure > CardFigure.nine;

        }

        private bool StreetFlash(CardSet cards)
        {
            if (!IsSameSuit(cards)) return false;
            cards.Sort();
            for (int i = 1; i < cards.Count; i++)
            {
                if cards
            }
        }
    }
}
