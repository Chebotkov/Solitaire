using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using SolitaireBCL;
using SolitaireGUI.Additional_Classes;

namespace SolitaireGUI.ViewModel
{
    class MainWindowVM : DependencyObject, INotifyPropertyChanged
    {
        public static readonly DependencyProperty MainDeckProperty;

        public static CardDeckSize DeckSize = CardDeckSize.Full;
        private int countOfCardsInMainStack = 3;
        private LightList<Card> mainList;
        private LightList<Card> tempList;
        private LightStack<Card> tempCardStack;
        private LightStack<UIElement> tempStackPanel;
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
        private BaseCommand shuffleMainListCommand;

        #region Constructors
        static MainWindowVM()
        {
            MainDeckProperty = DependencyProperty.Register("MainCardDeck", typeof(CardDeck), typeof(MainWindowVM), new PropertyMetadata(new CardDeck(DeckSize)));
        }

        public MainWindowVM()
        {
            NewGame();
            mainList.CollectionChanged += MainList_CollectionChanged;
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

        public BaseCommand ShuffleMainListCommand
        {
            get
            {
                return shuffleMainListCommand ??
                (shuffleMainListCommand = new BaseCommand(obj =>
                {
                    RefreshMainListPanel();
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

        public LightStack<UIElement> TempStackPanel
        {
            get { return tempStackPanel; }
            set
            {
                tempStackPanel = value;
                TempStackPanel.CollectionChanged += TempStackPanel_CollectionChanged;
                OnPropertyChanged(nameof(TempStackPanel));
            }
        }

        public LightStack<Card> TempCardStack
        {
            get { return tempCardStack; }
            set
            {
                tempCardStack = value;
                TempCardStack.CollectionChanged += TempCardStack_CollectionChanged;
                OnPropertyChanged(nameof(TempCardStack));
            }
        }

        public LightList<Card> MainList
        {
            get
            {
                return mainList;
            }
            set
            {
                mainList = value;
                MainList.CollectionChanged += MainList_CollectionChanged;
                OnPropertyChanged(nameof(MainList));
            }
        }

        public LightList<Card> TempList
        {
            get
            {
                return tempList;
            }
            set
            {
                tempList = value;
                OnPropertyChanged(nameof(TempList));
            }
        }

        public int CountOfCardsInMainStack
        {
            get
            {
                return countOfCardsInMainStack;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(String.Format("{0} can't be less than 1", nameof(CountOfCardsInMainStack)));
                }

                countOfCardsInMainStack = value;
                OnPropertyChanged(nameof(CountOfCardsInMainStack));
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
            //Deck shuffling, MainList and TempList Reset
            positionInMainDeck = -1;
            MainCardDeck.DeckShuffle();
            MainList = MainCardDeck.Deck;
            selectedCard = MainList.First;
            TempList = new LightList<Card>();

            //Output Stacks Reset
            FirstOutPutStack = new LightStack<Card>();
            SecondOutPutStack = new LightStack<Card>();
            ThirdOutPutStack = new LightStack<Card>();
            FourthOutPutStack = new LightStack<Card>();

            FirstOutPutStack.Push(new Card(CardSuit.Clovers, CardValue.Ace));
            ThirdOutPutStack.Push(new Card(CardSuit.Diamonds, CardValue.Five));
            TempStackPanel = new LightStack<UIElement>();
            TempCardStack = new LightStack<Card>();
        }

        private void TempStackPanel_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                TempCardStack.Pop();
            }
        }

        private void TempCardStack_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var temp = TempStackPanel;

                foreach (Card card in e.NewItems)
                {
                    Image image = new Image();
                    image.Source = GetBitmap(card);
                    temp.Push(image);
                }

                TempStackPanel = temp;
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                RefreshTempStackPanel();
            }
        }

        private void MainList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (mainList.Count == 0 && positionInMainDeck > 0)
            {
                MainList = TempList;
                TempList = new LightList<Card>();
                positionInMainDeck = -1;
            }
        }

        private void RefreshTempStackPanel()
        {
            if (TempCardStack.Count == 0)
            {
                if (TempList != null && TempList.Count > 0 && MainList.Count != 0)
                {
                    var stack = new LightStack<Card>();
                    int i = 0;
                    int upBorder = TempList.Count > countOfCardsInMainStack ? countOfCardsInMainStack : TempList.Count;

                    while (i < upBorder)
                    {                        
                        stack.Push(TempList.Last);
                        TempList.Remove(TempList.Last);
                        i++;
                    }
                    
                    stack.Reverse();
                    TempCardStack = stack;
                }
            }
        }

        private void RefreshMainListPanel()
        {
            var tempStack = new LightStack<Card>();

            if (TempCardStack is null)
            {
                TempCardStack = new LightStack<Card>();
                TempStackPanel = new LightStack<UIElement>();
            }

            else
            {
                if (MainList != null && MainList.Count > 0)
                {
                    int i = 0;
                    int upBorder = MainList.Count > countOfCardsInMainStack ? countOfCardsInMainStack : MainList.Count;

                    if (TempCardStack.Count > 0)
                    {
                        TempCardStack.Reverse();

                        while (TempCardStack.Count >= 1)
                        {
                            TempList.Add(TempCardStack.Pop());
                        }

                        TempList.Add(TempCardStack.Peek());
                    }

                    while (i < upBorder)
                    {
                        Card card = MainList.First;
                        TempList.Add(card);
                        positionInMainDeck++;

                        MainList.Remove(card);
                        i++;
                    }

                    TempCardStack.Pop();
                }
            }

            TempCardStack = tempStack;
        }

        private BitmapImage GetBitmap(string cardName)
        {
            return new BitmapImage(new Uri(String.Format("{0}\\{1}.png", CardManager.pathToCards, cardName), UriKind.Relative));
        }

        private BitmapImage GetBitmap(Card card)
        {
            return new BitmapImage(new Uri(CardManager.GetPathToCard(card), UriKind.Relative));
        }
    }
}
