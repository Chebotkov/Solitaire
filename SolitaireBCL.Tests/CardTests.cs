using System;
using NUnit.Framework;

namespace SolitaireBCL.Tests
{
    [TestFixture]
    public class CardTests
    {
        [TestCase(CardSuit.Clovers, CardColour.Black)]
        [TestCase(CardSuit.Diamonds, CardColour.Red)]
        [TestCase(CardSuit.Hearts, CardColour.Red)]
        [TestCase(CardSuit.Pikes, CardColour.Black)]
        public void GetCardColourTest(CardSuit cardSuit, CardColour expectedColour)
        {
            //Arrange
            Card card = new Card(cardSuit, CardValue.Ace);

            //Act
            CardColour result = card.GetCardColour();

            //Assert
            Assert.AreEqual(expectedColour, result);
        }
    }
}
