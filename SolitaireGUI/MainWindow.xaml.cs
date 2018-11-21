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
                SetDefault();
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
    }
}
