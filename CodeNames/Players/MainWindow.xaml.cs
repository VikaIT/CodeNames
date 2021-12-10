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

namespace Players
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int size = 5;
        private Button[,] buttons = new Button[size, size];
        public MainWindow()
        {
            InitializeComponent();
            AddingButtons();
            /* Button button1 = new Button();
             button1.Background = new SolidColorBrush(Colors.Azure);
             button1.Content = "Progtime";
             button1.Foreground = new SolidColorBrush(Colors.White);
             grid1.Children.Add(button1);
             //grid1.Children.Add(new Button());
             // назначить элемент на позицию грида
             Grid.SetColumn(button1, 1);
             Grid.SetRow(button1, 1);
             // подписка на событие Click
             // Метод = обработчик события
             button1.Click += Button1_Click;*/
        }
        public void AddingButtons()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Button b = new Button();
                    Grid.SetColumn(b, j);
                    Grid.SetRow(b, i);
                    buttons[i, j] = b;
                    buttons[i, j].Click += Button1_Click;
                    grid.Children.Add(b);
                }
            }
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
