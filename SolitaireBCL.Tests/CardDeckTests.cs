using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SolitaireBCL.Tests
{
    [TestFixture]
    public class CardDeckTests
    {
        [TestCase(CardDeckSize.Standard)]
        [TestCase(CardDeckSize.Full)]
        [TestCase(CardDeckSize.Standard)]
        [TestCase(CardDeckSize.Full)]
        public void DeckShuffleTests(CardDeckSize deckSize)
        {
            //Arrange
            CardDeck cardDeck = new CardDeck(deckSize);
            int[] result = new int[] { 0, 0, 0, 0 };
            int[] expected = deckSize == CardDeckSize.Full ? new int[] { 13, 13, 13, 13 } : new int []{ 9, 9, 9, 9 }; 

            //Act
            cardDeck.DeckShuffle();
            foreach (Card card in cardDeck.Deck)
            {
                switch (card.suit)  
                {
                    case CardSuit.Clovers:
                        {
                            result[0]++;
                            break;
                        }
                    case CardSuit.Diamonds:
                        {
                            result[1]++;
                            break;
                        }
                    case CardSuit.Hearts:
                        {
                            result[2]++;
                            break;
                        }
                    case CardSuit.Pikes:
                        {
                            result[3]++;
                            break;
                        }
                }
            }

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
