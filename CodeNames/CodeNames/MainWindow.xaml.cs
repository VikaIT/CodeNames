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
using System.Windows.Threading;

namespace Saper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int w = 9; // ширина поля 
        private static int h = 9; // высота поля
        private Button[,] buttons = new Button[h, w]; // ячейки: пустые, с бомбочкой, с числом            
        private int flags = 10;
        private int bombs_count = 10;
        private TextBlock flagsText = new TextBlock();
        private int openedCells = 0;
        TextBlock cellText = new TextBlock();
        Button buttonReset = new Button();
        int sec = 0;
        int min = 0;
        DispatcherTimer timer;
        TextBlock timerDisplay = new TextBlock();

        public MainWindow()
        {
            InitializeComponent();
            Game();
            timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 1, 0), DispatcherPriority.Background,
                t_Tick, Dispatcher.CurrentDispatcher);
        }

        public void t_Tick(object sender, EventArgs e)
        {
            timerDisplay.Text = Convert.ToString(min) + ":" + Convert.ToString(sec);

            sec++;
            if (sec == 60)
            {
                sec = 0;
                min++;
            }
        }
        public void Game()
        {
            Flags();
            AddingButtons();
            GenerateBombs();
            CheckBombs();

            timerDisplay.FontSize = 20;
            buttonReset.Visibility = Visibility.Hidden;
            buttonReset.Click += ButtonReset_Click;
            Grid.SetColumn(timerDisplay, 8);
            Grid.SetColumnSpan(timerDisplay, 3);
            grid.Children.Add(timerDisplay);
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            sec = 0;
            min = 0;
            timer.Start();

            grid.Children.Clear();
            Game();
        }

        // инициализация кнопок
        public void AddingButtons()
        {
            // флаги
            flagsText.FontSize = 20;
            Grid.SetColumn(flagsText, 0);
            Grid.SetRow(flagsText, 0);
            Grid.SetColumnSpan(flagsText, 4);
            grid.Children.Add(flagsText);
            // заполнение массива кнопок
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Button b = new Button();
                    Grid.SetColumn(b, j);
                    Grid.SetRow(b, i + 1);
                    b.FontSize = 1;
                    buttons[i, j] = b;
                    buttons[i, j].Click += Button_Click;
                    grid.Children.Add(b);
                }
            }
        }
        private void CountCells()
        {
            grid.Children.Remove(cellText);
            cellText.Text = "Открыто ячеек: " + openedCells + "/" + (h * w);
            cellText.FontSize = 20;
            Grid.SetColumn(cellText, 0);
            Grid.SetRow(cellText, 10);
            Grid.SetColumnSpan(cellText, 3);
            grid.Children.Add(cellText);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Button b_new = new Button();
                b_new.Content = "ф";
                Grid.SetColumn(b_new, Grid.GetColumn(button));
                Grid.SetRow(b_new, Grid.GetRow(button));
                grid.Children.Add(b_new);
                flags -= 1;
                Flags();
                button.IsEnabled = false;
                b_new.Click += B_new_Click;
                button.IsEnabled = true;
            }

            if ((string)button.Content != " ")
            {
                button.FontSize = 20;
                button.IsEnabled = false;
                // Game Over - LOSE
                if ((string)button.Content == "B")
                {
                    Lose();
                }
            }
            else
            {
                button.IsEnabled = false;
            }
            openedCells++;
            CountCells();
            //Game Over - WIN
            if (openedCells == (h * w - bombs_count))
            {
                Win();
            }
        }


        private void B_new_Click(object sender, RoutedEventArgs e)
        {
            Button f_button = sender as Button;
            grid.Children.Remove(f_button);
        }

        // добавление текстового блока, овечающего за кол-во флагов
        public void Flags()
        {

            flagsText.Text = "Ф" + flags.ToString() + " (Ф значит флаг)";

        }

        public void GenerateBombs()
        {
            Random random = new Random();
            int r1;
            int r2;
            for (int i = 0; i < bombs_count; i++)
            {
                do
                {
                    r1 = random.Next(0, h);
                    r2 = random.Next(0, w);
                } while ((string)buttons[r1, r2].Content == "B");

                buttons[r1, r2].Content = "B";
            }

        }

        public void CheckBombs()
        {
            int count = 0;

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    count = 0;
                    if ((string)buttons[i, j].Content != "B") // ЕСЛИ КНОПКА ПУСТАЯ
                    {
                        if (i != 0) // если есть кнопки над текущей чекаем их
                        {
                            if ((string)buttons[i - 1, j].Content == "B")// кнопка над текущей
                            {
                                count++;
                            }
                            if (j != 0)
                            {
                                if ((string)buttons[i - 1, j - 1].Content == "B")// вверх наискосок влево
                                {
                                    count++;
                                }
                            }
                            if (j != w - 1)
                            {
                                if ((string)buttons[i - 1, j + 1].Content == "B")// вверх вправо наискосок
                                {
                                    count++;
                                }
                            }
                        }
                        if (i != h - 1) // если есть кнопки над текущей чекаем их
                        {
                            if ((string)buttons[i + 1, j].Content == "B")// кнопка под текущей
                            {
                                count++;
                            }
                            if (j != 0)
                            {
                                if ((string)buttons[i + 1, j - 1].Content == "B")// вниз наискосок влево
                                {
                                    count++;
                                }
                            }
                            if (j != w - 1)
                            {
                                if ((string)buttons[i + 1, j + 1].Content == "B")// вниз вправо наискосок
                                {
                                    count++;
                                }
                            }
                        }
                        if (j != 0) // если есть кнопки слева от текущей чекаем их
                        {
                            if ((string)buttons[i, j - 1].Content == "B")// кнопка слева текущей
                            {
                                count++;
                            }
                        }
                        if (j != w - 1) // если есть кнопки слева от текущей чекаем их
                        {
                            if ((string)buttons[i, j + 1].Content == "B")// кнопка справа от текущей
                            {
                                count++;
                            }
                        }
                        if (count == 0)
                        {
                            buttons[i, j].Content = " ";
                        }
                        else
                        {
                            buttons[i, j].Content = count.ToString();
                        }
                    }
                }
            }
        }

        public void Lose()
        {
            TextBlock text = new TextBlock();
            text.FontSize = 30;
            Grid.SetColumn(text, 3);
            Grid.SetColumnSpan(text, 4);

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    buttons[i, j].IsEnabled = false;
                    if ((string)buttons[i, j].Content == "B")
                    {
                        buttons[i, j].FontSize = 20;
                        buttons[i, j].Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
            }
            timer.Stop();

            cellText.Visibility = Visibility.Hidden;
            buttonReset.Visibility = Visibility.Visible;
            Grid.SetRow(buttonReset, 10);
            buttonReset.Content = "ПЕРЕИГРАТЬ";

            text.Text = "Game Over - YOU LOSE!";
            grid.Children.Add(text);
            grid.Children.Add(buttonReset);
        }

        public void Win()
        {
            timer.Stop();

            cellText.Visibility = Visibility.Hidden;
            buttonReset.Visibility = Visibility.Visible;
            Grid.SetRow(buttonReset, 10);
            buttonReset.Content = "ПЕРЕИГРАТЬ";

            TextBlock text = new TextBlock();
            text.FontSize = 25;
            Grid.SetColumn(text, 3);
            Grid.SetColumnSpan(text, 4);
            text.Text = "Congratulations! YOU WON!";
            grid.Children.Add(text);
        }
    }
}
