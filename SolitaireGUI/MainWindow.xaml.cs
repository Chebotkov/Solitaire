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
                foreach (Card element in stack)
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
            Close();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            MainStack.Children.Clear();
        }

        private void FirstOutPutStack_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LightStack<Card> stack = FirstOutPutStack.DataContext as LightStack<Card>;
            if (stack == null)
            {
                FirstOutPutStack.Children.Clear();
                Image image = new Image();
                Thickness margin = image.Margin;
                margin.Left = 12;
                margin.Top = 20;
                image.Margin = margin;
                FirstOutPutStack.Children.Add(image);
            }
            else
            {
                Image image = new Image();
                Thickness margin = image.Margin;
                margin.Left = 1;
                margin.Top = 20;
                image.Margin = margin;
                image.Source = GetBitmap(stack.Peek());
                FirstOutPutStack.Children.Add(image);
            }
        }

        private void SecondOutPutStack_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LightStack<Card> stack = SecondOutPutStack.DataContext as LightStack<Card>;
            if (stack == null)
            {
                SecondOutPutStack.Children.Clear();
                Image image = new Image();
                Thickness margin = image.Margin;
                margin.Left = 12;
                margin.Top = 20;
                image.Margin = margin;
                SecondOutPutStack.Children.Add(image);
            }
            else
            {
                Image image = new Image();
                Thickness margin = image.Margin;
                margin.Left = 12;
                margin.Top = 20;
                image.Margin = margin;
                image.Source = GetBitmap(stack.Peek());
                SecondOutPutStack.Children.Add(image);
            }
        }

        private void ThirdOutPutStack_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LightStack<Card> stack = ThirdOutPutStack.DataContext as LightStack<Card>;
            if (stack == null)
            {
                SecondOutPutStack.Children.Clear();
                Image image = new Image();
                Thickness margin = image.Margin;
                margin.Left = 12;
                margin.Top = 20;
                image.Margin = margin;
                ThirdOutPutStack.Children.Add(image);
            }
            else
            {
                Image image = new Image();
                Thickness margin = image.Margin;
                margin.Left = 12;
                margin.Top = 20;
                image.Margin = margin;
                image.Source = GetBitmap(stack.Peek());
                ThirdOutPutStack.Children.Add(image);
            }
        }

        private void FourthOutPutStack_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LightStack<Card> stack = FourthOutPutStack.DataContext as LightStack<Card>;
            if (stack == null)
            {
                SecondOutPutStack.Children.Clear();
                Image image = new Image();
                Thickness margin = image.Margin;
                margin.Left = 12;
                margin.Top = 20;
                image.Margin = margin;
                FourthOutPutStack.Children.Add(image);
            }
            else
            {
                Image image = new Image();
                Thickness margin = image.Margin;
                margin.Left = 12;
                margin.Top = 20;
                image.Margin = margin;
                image.Source = GetBitmap(stack.Peek());
                FourthOutPutStack.Children.Add(image);
            }
        }
    }
}
