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

namespace SimpleSnakeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        System.Windows.Threading.DispatcherTimer timer;
        GameState gameState;


        public MainWindow()
        {
            InitializeComponent ();
            myCanvas.Children.Add(DrawGrid.MyDrawGrid());
            gameState = new GameState(myCanvas);
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 260);
            timer.Tick += Timer_Tick;
           
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            gameState.IsEat();
            gameState.GenNewSnakeNode();
            if (gameState.IsGameOver())
            {
                gameState.state = GameState.State.STOP;
                timer.Stop();
                MessageBox.Show("End","Tips",MessageBoxButton.OK);
                return;
            }
        }

        private void MenuFile_NewGame_Click(object sender, RoutedEventArgs e)
        {
            gameState.StartGame();
            timer.Start();
            gameState.state = GameState.State.GAMEING;
            MenuControl_Pause.Header = "Pause";
        }

        private void myCanvas_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (gameState.direction != GameState.Direction.RIGHT)
                    {
                        gameState.direction = GameState.Direction.LEFT;
                    }
                    break;

                case Key.Right:
                    if (gameState.direction != GameState.Direction.LEFT)
                    {
                        gameState.direction = GameState.Direction.RIGHT;
                    }
                    break;

                case Key.Up:
                    if (gameState.direction != GameState.Direction.DOWN)
                    {
                        gameState.direction = GameState.Direction.UP;
                    }
                    break;

                case Key.Down:
                    if (gameState.direction != GameState.Direction.UP)
                    {
                        gameState.direction = GameState.Direction.DOWN;
                    }
                    break;

                case Key.Escape:
                    Application.Current.Shutdown();
                    break;

                case Key.Space:
                    if (gameState.state == GameState.State.NONE)
                        return;

                    if (gameState.state == GameState.State.PAUSE)
                    {
                        gameState.state = GameState.State.GAMEING;
                        timer.Start();
                        MenuControl_Pause.Header = "Pause";
                    }
                    else if (gameState.state == GameState.State.GAMEING)
                    {
                        gameState.state = GameState.State.PAUSE;
                        timer.Stop();
                        MenuControl_Pause.Header = "Continue";
                    }
                    break;
            }
        }

        private void MenuFile_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuControl_Pause_Click(object sender, RoutedEventArgs e)
        {
            if (gameState.state == GameState.State.GAMEING)
            {
                gameState.state = GameState.State.PAUSE;
                timer.Stop();
                MenuControl_Pause.Header = "Continue";
            }
            else if (gameState.state == GameState.State.PAUSE)
            {
                gameState.state = GameState.State.GAMEING;
                timer.Start();
                MenuControl_Pause.Header = "Pause";
            }
        }
    }

    

}
