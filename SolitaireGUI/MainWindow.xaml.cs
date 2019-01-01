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
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            if (sender != null)
            {
                Image current = sender as Image;
                if (current != null)
                {
                    Image card = e.Data.GetData(typeof(Image)) as Image;
                    if (card != null)
                    {
                        FirstOutPutStack.Source = card.Source;
                        StackPanel ParentStack = current.Parent as StackPanel;
                        if (ParentStack != null)
                        {
                            var deleted = ((MainWindowVM)this.DataContext).TempStackPanel.Pop();
                            ParentStack.Children.Remove(current);
                        }
                    }
                }
            }
        }

        private void MainStack_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MainStack.Children.Clear();
            foreach (UIElement element in ((MainWindowVM)this.DataContext).TempStackPanel)
            {
                if (element is Image)
                {
                    Image newChild = (Image)element;
                    if (MainStack.Children.Count == 0)
                    {
                        Thickness margin = newChild.Margin;
                        margin.Left = 12;
                        margin.Top = 20;
                        newChild.Margin = margin;
                        MainStack.Children.Add(element);
                    }
                    else
                    {
                        Thickness margin = newChild.Margin;
                        margin.Left = -35;
                        margin.Top = 20;
                        newChild.Margin = margin;
                        MainStack.Children.Add(element);
                    }
                }
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
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
                        switch (ParentStack.Name)
                        {
                            case "MainStack":
                                {
                                    DragDrop.DoDragDrop(current, current, DragDropEffects.Move);
                                    break;
                                }
                        }
                    }
                }
            }
        }
    }
}
