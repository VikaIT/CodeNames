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

namespace CodeNames
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int size = 5;
        private Button[,] buttons = new Button[size, size];
        private TextBox chat;
        public MainWindow()
        {
            InitializeComponent();
            AddingElems();

        }
        public void AddingElems()
        {
            chat = new TextBox();
            Grid.SetColumn(chat, size);
            Grid.SetRow(chat, 0);
            Grid.SetRowSpan(chat, size);
            chat.TextWrapping = TextWrapping.Wrap;
            chat.Visibility = Visibility.Visible;
            grid1.Children.Add(chat);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Button b = new Button();
                    Grid.SetColumn(b, j);
                    Grid.SetRow(b, i);
                    buttons[i, j] = b;
                    buttons[i, j].Click += Button1_Click;
                    grid1.Children.Add(b);
                }
            }
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
