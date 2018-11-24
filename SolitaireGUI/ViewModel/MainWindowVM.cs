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
        //Add for each property current chosen value
        private LightStack<Card> firstOutPutStack;
        private LightStack<Card> secondOutPutStack;
        private LightStack<Card> thirdOutPutStack;
        private LightStack<Card> fourthOutPutStack;
        private LightStack<Card> firstAdditionalStack;
        private LightStack<Card> secondAdditionalStack;
        private LightStack<Card> thirdAdditionalStack;
        private LightStack<Card> fourthAdditionalStack;
        private LightStack<Card> fifthAdditionalStack;
        private LightStack<Card> sixthAdditionalStack;
        private LightStack<Card> seventhAdditionalStack;
        private LightList<Card> mainDeck;
        private Card selectedCard;
        private int positionInMainDeck = -1;
        private BaseCommand newGameCommand;
        private BaseCommand shuffleMainStackCommand;

        #region Constructors
        static MainWindowVM()
        {
            MainDeckProperty = DependencyProperty.Register("MainCardDeck", typeof(CardDeck), typeof(MainWindowVM), new PropertyMetadata(new CardDeck(DeckSize)));
        }

        public MainWindowVM()
        {
            NewGame();
        }
        #endregion

        #region Commands
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
                        MainStack = null;
                    }
                    else
                    {
                        int startIndex = positionInMainDeck + 1;
                        positionInMainDeck = positionInMainDeck + CountOfCardsInMainStack < mainDeck.Count ? positionInMainDeck + CountOfCardsInMainStack : positionInMainDeck + mainDeck.Count - positionInMainDeck - 1;
                        MainStack = GetNewMainStack(startIndex, positionInMainDeck);
                    }
                }));
            }
            private set { }
        }
        #endregion

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

        public LightStack<Card> FirstOutPutStack
        {
            get
            {
                return firstOutPutStack;
            }
            set
            {
                firstOutPutStack = value;
                OnPropertyChanged(nameof(FirstOutPutStack));
            }
        }

        public LightStack<Card> SecondOutPutStack
        {
            get
            {
                return secondOutPutStack;
            }
            set
            {
                secondOutPutStack = value;
                OnPropertyChanged(nameof(SecondOutPutStack));
            }
        }

        public LightStack<Card> ThirdOutPutStack
        {
            get
            {
                return thirdOutPutStack;
            }
            set
            {
                thirdOutPutStack = value;
                OnPropertyChanged(nameof(ThirdOutPutStack));
            }
        }

        public LightStack<Card> FourthOutPutStack
        {
            get
            {
                return fourthOutPutStack;
            }
            set
            {
                fourthOutPutStack = value;
                OnPropertyChanged(nameof(FourthOutPutStack));
            }
        }


        public LightStack<Card> FirstAdditionalStack
        {
            get
            {
                return firstAdditionalStack;
            }
            set
            {
                firstAdditionalStack = value;
                OnPropertyChanged(nameof(FirstAdditionalStack));
            }
        }

        public LightStack<Card> SecondAdditionalStack
        {
            get
            {
                return secondAdditionalStack;
            }
            set
            {
                secondAdditionalStack = value;
                OnPropertyChanged(nameof(SecondAdditionalStack));
            }
        }

        public LightStack<Card> ThirdAdditionalStack
        {
            get
            {
                return thirdAdditionalStack;
            }
            set
            {
                thirdAdditionalStack = value;
                OnPropertyChanged(nameof(ThirdAdditionalStack));
            }
        }

        public LightStack<Card> FourthAdditionalStack
        {
            get
            {
                return fourthAdditionalStack;
            }
            set
            {
                fourthAdditionalStack = value;
                OnPropertyChanged(nameof(FourthAdditionalStack));
            }
        }

        public LightStack<Card> FifthAdditionalStack
        {
            get
            {
                return fifthAdditionalStack;
            }
            set
            {
                fifthAdditionalStack = value;
                OnPropertyChanged(nameof(FifthAdditionalStack));
            }
        }

        public LightStack<Card> SixthAdditionalStack
        {
            get
            {
                return sixthAdditionalStack;
            }
            set
            {
                sixthAdditionalStack = value;
                OnPropertyChanged(nameof(SixthAdditionalStack));
            }
        }

        public LightStack<Card> SeventhAdditionalStack
        {
            get
            {
                return seventhAdditionalStack;
            }
            set
            {
                seventhAdditionalStack = value;
                OnPropertyChanged(nameof(SeventhAdditionalStack));
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
            selectedCard = MainDeck.First;
            MainCardDeck.DeckShuffle();
            MainDeck = MainCardDeck.Deck;
            selectedCard = MainDeck.First;

            positionInMainDeck = -1;

            //MainStackReset
            MainStack = null;

            //Output Stacks Reset
            FirstOutPutStack = new LightStack<Card>();
            SecondOutPutStack = new LightStack<Card>();
            ThirdOutPutStack = new LightStack<Card>();
            FourthOutPutStack = new LightStack<Card>();
            
            FirstOutPutStack.Push(new Card(CardSuit.Clovers, CardValue.Ace));
            ThirdOutPutStack.Push(new Card(CardSuit.Diamonds, CardValue.Five));
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
