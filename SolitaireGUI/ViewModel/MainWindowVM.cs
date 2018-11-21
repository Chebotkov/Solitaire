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
        public static CardDeckSize DeckSize = CardDeckSize.Full;
        public int CountOfCardsInMainStack = 3;
        private LightStack<Card> mainStack;
        private LightList<Card> mainDeck;
        private Card selectedCard;
        private int positionInMainDeck = -1;
        private BaseCommand newGameCommand;
        private BaseCommand shuffleMainStackCommand;

        public BaseCommand NewGameCommand
        {
            get
            {
                return newGameCommand ??
                (newGameCommand = new BaseCommand(obj =>
                {
                    NewGame();
                }));
            }
            private set { }
        }

        public BaseCommand ShuffleMainStackCommand
        {
            get
            {
                return shuffleMainStackCommand ??
                (shuffleMainStackCommand = new BaseCommand(obj =>
                {
                    if (positionInMainDeck >= MainDeck.Count - 1)
                    {
                        positionInMainDeck = -1;
                    }

                    int startIndex = positionInMainDeck + 1;
                    positionInMainDeck = positionInMainDeck + CountOfCardsInMainStack < mainDeck.Count ? positionInMainDeck + CountOfCardsInMainStack : positionInMainDeck + mainDeck.Count - positionInMainDeck - 1;
                    MainStack = GetNewMainStack(startIndex, positionInMainDeck);
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
            MainDeckProperty = DependencyProperty.Register("MainCardDeck", typeof(CardDeck), typeof(MainWindowVM), new PropertyMetadata(new CardDeck(DeckSize)));
        }

        public MainWindowVM()
        {
            selectedCard = MainDeck.First;
            mainDeck = ((CardDeck)GetValue(MainDeckProperty)).Deck;
        }

        public LightList<Card> MainDeck
        {
            get { return ((CardDeck)GetValue(MainDeckProperty)).Deck; }
            set
            {
                mainDeck = value;
                OnPropertyChanged(nameof(MainDeck));
            }
        }

        public LightStack<Card> MainStack
        {
            get
            {
                return mainStack;
            }
            set
            {
                mainStack = value;
                OnPropertyChanged(nameof(MainStack));
            }
        }

        public CardDeck MainCardDeck
        {
            get { return (CardDeck)GetValue(MainDeckProperty); }
            set { SetValue(MainDeckProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void NewGame()
        {
            //Deck shuffling
            MainCardDeck.DeckShuffle();
            MainDeck = MainCardDeck.Deck;
            selectedCard = MainDeck.First;

            positionInMainDeck = -1;

            //MainStackReset
            MainStack = null;
        }

        private LightStack<Card> GetNewMainStack(int startIndex, int currentIndex)
        {
            LightStack<Card> newStack = new LightStack<Card>();
            for (int i = startIndex; i <= currentIndex; i++)
            {
                newStack.Push(MainDeck[i]);
            }

            return newStack;
        }
    }
}
