using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireBCL
{
    public enum CardDeckSize {Standard = 36, Full = 52}

    public class CardDeck
    {
        private Random random;
        public LightList<Card> Deck { get; private set; }
        public readonly CardDeckSize DeckSize;
        public CardDeck(CardDeckSize cardDeckSize)
        {
            DeckSize = cardDeckSize;
            Deck = CardDeckInitialization();
            random = new Random();
        }

        private LightList<Card> CardDeckInitialization()
        {
            LightList<Card> list = new LightList<Card>();
            if (DeckSize == CardDeckSize.Standard)
            {
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                    {
                        if (value > CardValue.Five)
                        {
                            list.Add(new Card(suit, value));
                        }
                    }
                    list.Add(new Card(suit, CardValue.Ace));
                }
            }
            else
            {
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                    {
                        list.Add(new Card(suit, value));
                    }
                }
            }

            return list;
        }

        public void DeckShuffle()
        {
            LightList<Card> newList = new LightList<Card>();

            while (newList.Count < (int)DeckSize)
            {
                Card card = new Card((CardSuit)random.Next(1, 5), (CardValue)random.Next(1, 14));

                if (!newList.Contains(card))
                {
                    newList.Add(card);
                }
            }

            Deck = newList;
        }
    }
}
