using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolitaireBCL;

namespace SolitaireGUI.Additional_Classes
{
    public static class CardManager
    {
        public static string pathToCards = @"Resourses\Cards";
        public static string GetPathToCard(Card card)
        {
            return String.Format("{0}\\{1}{2}.png", pathToCards, card.value.ToString(), card.suit.ToString());
        }
    }
}
