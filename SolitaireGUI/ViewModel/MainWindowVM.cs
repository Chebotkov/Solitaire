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

        public static DependencyProperty MainStackPanelProperty { get; }

        public static CardDeckSize DeckSize = CardDeckSize.Full;
        private int countOfCardsInMainStack = 3;
        private LightStack<Card> mainStack;
        private int mainStackPreviousCount;
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
            MainStackPanelProperty = DependencyProperty.Register("MainStackPanel", typeof(StackPanel), typeof(MainWindowVM));
        }

        public MainWindowVM()
        {
            NewGame();
            mainStack.CollectionChanged += MainStack_CollectionChanged;
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
                    if (MainStack != null)
                    {
                        int upBorder = MainStack.Count + CountOfCardsInMainStack < mainDeck.Count ? (MainStack.Count + CountOfCardsInMainStack) : (MainStack.Count + (mainDeck.Count - MainStack.Count - 1));
                        for (int i = MainStack.Count; i < upBorder; i++)
                        {
                            MainStack.Push(mainDeck[i]);
                        }
                        MessageBox.Show("Pushed");
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

        public StackPanel MainStackPanel
        {
            get { return (StackPanel)GetValue(MainStackPanelProperty); }
            set
            {
                SetValue(MainStackPanelProperty, value);
                OnPropertyChanged(nameof(MainStackPanel));
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
                RefreshMainStackPanel();
                MessageBox.Show("Refreshed");
                OnPropertyChanged(nameof(MainStack));
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
            //Deck shuffling
            MainCardDeck.DeckShuffle();
            MainDeck = MainCardDeck.Deck;
            selectedCard = MainDeck.First;

            positionInMainDeck = -1;

            //MainStackReset
            MainStack = new LightStack<Card>();
            MainStack.CollectionChanged += MainStack_CollectionChanged;
            RefreshMainStackPanel();

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

        private void MainStack_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                MainDeck.Remove((Card)e.OldItems[0]);
            }

            if (mainStack.Count == 0 && positionInMainDeck > 0)
            {
                var temp = mainStack.GetReversedVersion();
                temp.Push(mainDeck[--positionInMainDeck]);
                MainStack = temp.GetReversedVersion();
            }
        }

        private void RefreshMainStackPanel()
        {
            StackPanel stackPanel = new StackPanel();

            if (MainStackPanel is null)
            {
                MainStackPanel = new StackPanel();
            }

            if (MainStackPanel.Children.Count == 0 && MainStack.Count > 0)
            {
                Image image = new Image();
                Thickness margin = image.Margin;
                margin.Left = 12;
                margin.Top = 20;
                image.Margin = margin;
                image.Source = GetBitmap(MainStack.Peek());
                stackPanel.Children.Add(image);
            }
            else
            {
                if (MainStack == null || MainStack.Count == 0)
                {
                    stackPanel.Children.Clear();
                }
                else
                {
                    int i = 0;
                    LightStack<Card> Temp = new LightStack<Card>();
                    while (i < CountOfCardsInMainStack)
                    {
                        Temp.Push(MainStack.Pop());
                        i++;
                    }

                    foreach (Card element in Temp)
                    {
                        if (stackPanel.Children.Count == 0)
                        {
                            Image image = new Image();
                            Thickness margin = image.Margin;
                            margin.Left = 12;
                            margin.Top = 20;
                            image.Margin = margin;
                            image.Source = GetBitmap(element);
                            stackPanel.Children.Add(image);
                        }
                        else
                        {
                            Image image = new Image();
                            image.Source = GetBitmap(element);
                            Thickness margin = image.Margin;
                            margin.Left = -35;
                            margin.Top = 20;
                            image.Margin = margin;
                            stackPanel.Children.Add(image);
                        }
                    }
                }
            }

            MainStackPanel = stackPanel;
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
