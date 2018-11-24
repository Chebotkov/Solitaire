using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SolitaireGUI.ViewModel;
using SolitaireBCL;
using SolitaireGUI.Additional_Classes;

namespace SolitaireGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainWindowVM();
        }

        private void MainStack_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            void SetDefault()
            {
                Image image = new Image();
                Thickness margin = image.Margin;
                margin.Left = 12;
                margin.Top = 20;
                image.Margin = margin;
                MainStack.Children.Clear();
                MainStack.Children.Add(image);
            }
            LightStack<Card> stack = MainStack.DataContext as LightStack<Card>;
            MainStack.Children.Clear();
            if (stack == null)
            {
                MainStack.Children.Clear();
            }
            else
            {
                foreach (Card element in stack.GetReversedVersion())
                {
                    if (MainStack.Children.Count == 0)
                    {
                        SetDefault();
                        (MainStack.Children[0] as Image).Source = GetBitmap(element);
                    }
                    else
                    {
                        Image image = new Image();
                        image.Source = GetBitmap(element);
                        Thickness margin = image.Margin;
                        margin.Left = -35;
                        margin.Top = 20;
                        image.Margin = margin;
                        MainStack.Children.Add(image);
                    }
                }
            }
        }

        private BitmapImage GetBitmap(string cardName)
        {
            return new BitmapImage(new Uri(String.Format("{0}\\{1}.png", CardManager.pathToCards, cardName), UriKind.Relative));
        }

        private BitmapImage GetBitmap(Card card)
        {
            return new BitmapImage(new Uri(CardManager.GetPathToCard(card), UriKind.Relative));
        }

        private void MenuExitItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            MainStack.Children.Clear();
        }

        private void OutPutStack_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Image current = sender as Image;
            if (current != null)
            {
                LightStack<Card> stack = current.DataContext as LightStack<Card>;
                if (stack != null && stack.Count > 0)
                {
                    current.Source = GetBitmap(stack.Peek());
                }
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                Image current = sender as Image;
                if (current != null)
                {
                    StackPanel ParentStack = current.Parent as StackPanel;
                    Card card;
                    LightStack<Card> stack;
                    if (ParentStack != null)
                    {
                        stack = ParentStack.DataContext as LightStack<Card>;
                        if (ParentStack.Children.Count > 0 && stack != null && stack.Count > 0)
                        {
                            card = stack.Pop();
                            ParentStack.Children.Remove(ParentStack.Children[ParentStack.Children.Count - 1]);
                            DragDrop.DoDragDrop(current, card, DragDropEffects.Move);
                        }
                    }
                    else
                    {
                        if (current.Parent is Grid)
                        {
                            stack = current.DataContext as LightStack<Card>;
                            if (stack is null)
                            {
                                current.Source = GetBitmap("EmptyBack");
                            }
                            else
                            {
                                if (stack.Count > 0)
                                {
                                    card = stack.Pop();
                                    if (stack.Count > 0)
                                    {
                                        current.Source = GetBitmap(stack.Peek());
                                    }

                                    DragDrop.DoDragDrop(current, card, DragDropEffects.Move);
                                }

                                if (stack.Count == 0)
                                {
                                    current.Source = GetBitmap("EmptyBack");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            if (sender != null)
            {
                Image current = sender as Image;
                if (current != null)
                {
                    Card card = e.Data.GetData(typeof(Card)) as Card;
                    if (card != null)
                    {
                        LightStack<Card> stack = current.DataContext as LightStack<Card>;
                        if (stack is null)
                        {
                            stack = new LightStack<Card>();
                        }

                        stack.Push(card);
                        current.Source = GetBitmap(card);
                    }
                }
            }
        }
    }
}
