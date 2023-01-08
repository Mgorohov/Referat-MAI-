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

namespace tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer Timer;
        Board myBoard;

        public MainWindow()
        {
            InitializeComponent();
        }

        void MainWindow_Initialized(object sender, EventArgs e)
        {
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(GameTick);                        
        }

        private void GameTick(object sender, EventArgs e)
        {
            try
            {
                Score.Content = myBoard.Score.ToString("0000000");
                Lines.Content = myBoard.LinesFilled.ToString("0000000");
                myBoard.CurrTetrisMoveDown();
                if (myBoard.checkLastRow())
                {
                    Timer.Stop();
                    MainGrid.Children.Clear();
                    Lines.FontSize = 15;
                    Lines.Content = "Your score is " + Score.Content;
                    Score.Content = "GAME OVER!";
                    Score.Width = 184;
                    Score.Foreground = Brushes.Red;
                }
            }
            catch { }
        }

        private void GameStart()
        {
            Score.Foreground = Brushes.Black;
            Lines.FontSize = 35;
            Score.Width = 150;
            MainGrid.Children.Clear();
            myBoard = new Board(MainGrid);
            Timer.Start();
        }

        private void GamePause()
        {
            if (Timer.IsEnabled)
            {
                Pause.Content = "Start";
                Timer.Stop();
            }
            else
            {
                Pause.Content = "Pause";
                Timer.Start();
            }
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (Timer.IsEnabled) myBoard.CurrTetrisMoveLeft();  
                    break;
                case Key.Right:
                    if (Timer.IsEnabled) myBoard.CurrTetrisMoveRight();  
                    break;
                case Key.Down:
                    if (Timer.IsEnabled) myBoard.CurrTetrisMoveDown();  
                    break;
                case Key.Up:
                    if (Timer.IsEnabled) myBoard.CurrTetrisMoveRotate();  
                    break;
                case Key.Enter:                  
                    GameStart();
                    break;
                case Key.LeftShift:
                    GamePause();                    
                    break;
                case Key.Escape:
                    Close();
                    break;
                default:
                    break;
            }
        }
               
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if ((string)(b.Content) == "New Game")
                Timer.Interval = new TimeSpan(0, 0, 0, 0, 600);
            else if ((string)(b.Content) == "Level1")
                Timer.Interval = new TimeSpan(0, 0, 0, 0, 600);
            else if ((string)(b.Content) == "Level2")
                Timer.Interval = new TimeSpan(0, 0, 0, 0, 400);
            else if ((string)(b.Content) == "Level3")
                Timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            GameStart();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            GamePause();                       
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
