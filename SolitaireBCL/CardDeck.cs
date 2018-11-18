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
        public LightStack<Card> Deck { get; private set; }
        public readonly CardDeckSize DeckSize;
        public CardDeck(CardDeckSize cardDeckSize)
        {
            DeckSize = cardDeckSize;
            Deck = CardDeckInitialization();
        }

        private LightStack<Card> CardDeckInitialization()
        {
            LightStack<Card> stack = new LightStack<Card>();
            if (DeckSize == CardDeckSize.Standard)
            {
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                    {
                        if (value > CardValue.Five)
                        {
                            stack.Push(new Card(suit, value));
                        }
                    }
                    stack.Push(new Card(suit, CardValue.Ace));
                }
            }
            else
            {
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                    {
                        stack.Push(new Card(suit, value));
                    }
                }
            }

            return stack;
        }

        public void DeckShuffle()
        {
            throw new NotImplementedException();

            Random random = new Random();
            LightStack<Card> newDeck = new LightStack<Card>();

            while(Deck.Count > 0)
            {

            }
        }
    }
}
