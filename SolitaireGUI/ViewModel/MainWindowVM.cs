using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SolitaireBCL;
using SolitaireGUI.Additional_Classes;

namespace SolitaireGUI.ViewModel
{
    class MainWindowVM : DependencyObject, INotifyPropertyChanged
    {
        public static readonly DependencyProperty MainDeckProperty;
        public static readonly CardDeck cardDeck;
        private Card selectedCard;
        private BaseCommand shuffleCommand;

        public BaseCommand ShuffleCommand
        {
            get
            {
                return shuffleCommand ??
                (shuffleCommand = new BaseCommand(obj =>
                {
                    cardDeck.DeckShuffle();
                    MainDeck = cardDeck.Deck;
                    selectedCard = MainDeck.First;
                }));
            }
            private set { }
        }


        public Card SelectedCard
        {
            get
            {
                return selectedCard;
            }
            private set
            {
                selectedCard = value;
                OnPropertyChanged(nameof(SelectedCard));
            }
        }

        static MainWindowVM()
        {
            cardDeck = new CardDeck(CardDeckSize.Full);
            MainDeckProperty = DependencyProperty.Register("MainDeck", typeof(LightList<Card>), typeof(MainWindowVM), new PropertyMetadata(cardDeck.Deck));
        }

        public MainWindowVM()
        {
            selectedCard = MainDeck.First;
        }

        public LightList<Card> MainDeck
        {
            get { return (LightList<Card>)GetValue(MainDeckProperty); }
            set { SetValue(MainDeckProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
