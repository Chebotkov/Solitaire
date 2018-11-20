using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireBCL
{
    public enum CardColour { Red, Black }
    public enum CardSuit { Pikes = 1, Hearts, Clovers, Diamonds }
    public enum CardValue { Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }
     
    public static class CardInfo
    {
        public static Dictionary<CardSuit, CardColour> Cards = new Dictionary<CardSuit, CardColour>()
        {
            { CardSuit.Clovers, CardColour.Black },
            { CardSuit.Diamonds, CardColour.Red },
            { CardSuit.Hearts, CardColour.Red },
            { CardSuit.Pikes, CardColour.Black }
        };
    }

    public class Card : IEquatable<Card>
    {
        public readonly CardSuit suit;
        public readonly CardValue value;
        
        public Card(CardSuit suit, CardValue value)
        {
            this.suit = suit;
            this.value = value;
        }

        public CardColour GetCardColour()
        {
            return CardInfo.Cards[suit];
        }

        public bool Equals(Card other)
        {
            if (other.suit == this.suit)
            {
                if (other.value == this.value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
