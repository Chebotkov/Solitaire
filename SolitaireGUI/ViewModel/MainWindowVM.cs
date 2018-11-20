using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SolitaireBCL;

namespace SolitaireGUI.ViewModel
{
    class MainWindowVM : DependencyObject
    {
        public static readonly DependencyProperty MainDeckProperty;

        public static readonly CardDeck cardDeck;

        static MainWindowVM()
        {
            cardDeck = new CardDeck(CardDeckSize.Full);
            MainDeckProperty = DependencyProperty.Register("MainDeck", typeof(LightList<Card>), typeof(MainWindowVM), new PropertyMetadata(cardDeck.Deck));
        }

        public LightList<Card> MainDeck
        {
            get { return (LightList<Card>)GetValue(MainDeckProperty); }
            set { SetValue(MainDeckProperty, value); }
        }
    }
}
