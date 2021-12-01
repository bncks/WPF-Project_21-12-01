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

namespace Flappy_Rabbit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();

        double score;
        int gravity = 8;
        bool gameOver;
        Rect flappyRabbitHitBox;

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Tick += MainEventTimer;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            StartGame();
        }

        public MainWindow(bool gameOver)
        {
            this.gameOver = gameOver;
        }

        private void MainEventTimer(object sender, EventArgs e)
        {
            //pontok kiírása
            txtScore.Content = "Elért pontok: " + score;

            //A nyúl "hitbox" elkészítése
            flappyRabbitHitBox = new Rect(Canvas.GetLeft(FlappyRabbit), Canvas.GetTop(FlappyRabbit), FlappyRabbit.Width -5, FlappyRabbit.Height);

            Canvas.SetTop(FlappyRabbit, Canvas.GetTop(FlappyRabbit) + gravity);

            //játék vége
            if (Canvas.GetTop(FlappyRabbit) < -5 || Canvas.GetTop(FlappyRabbit) > 458)
            {
                EndGame();
            }

            //répák, felhők mozgása
            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "cr1" || (string)x.Tag == "cr2" || (string)x.Tag == "cr3")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) -5);

                    if (Canvas.GetLeft(x) < -100)
                    {
                        Canvas.SetLeft(x, 800);

                        score += .5;
                    }

                    //répa "hitboxa"
                    Rect carrotHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (flappyRabbitHitBox.IntersectsWith(carrotHitBox))
                    {
                        EndGame();
                    }
                }

                if ((string)x.Tag == "cloud")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) -2);

                    if (Canvas.GetLeft(x) < -250)
                    {
                        Canvas.SetLeft(x, 550);
                    }
                }
            }
        }

        //irányítás
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                FlappyRabbit.RenderTransform = new RotateTransform(-20, FlappyRabbit.Width / 2, FlappyRabbit.Height / 2);
                gravity = -6;
            }
        }

        //lépés
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            FlappyRabbit.RenderTransform = new RotateTransform(5, FlappyRabbit.Width / 2, FlappyRabbit.Height / 2);
            gravity = 6;
        }

        private void StartGame()
        {
            
            MyCanvas.Focus();
            int temp = 300;
            score = 0;
            gameOver = false;
            Canvas.SetTop(FlappyRabbit, 190);

            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "cr1")
                {
                    Canvas.SetLeft(x, 500);
                }

                if ((string)x.Tag == "cr2")
                {
                    Canvas.SetLeft(x, 800);
                }

                if ((string)x.Tag == "cr3")
                {
                    Canvas.SetLeft(x, 1100);
                }

                if ((string)x.Tag == "cloud")
                {
                    Canvas.SetLeft(x, 300 + temp);
                    temp = 800;
                }
            }

            gameTimer.Start();


        }

        //játék vége, hibaüzenet kiíratása
        private void EndGame()
        {
            gameTimer.Stop();
            gameOver = true;
            MessageBox.Show("Sajnáljuk, vége a játéknak. Elért pontok: " + score, "*** Game Over ***");
            
            //újrakezdés felvetése, vagy játék vége felugró ablakból
            MessageBoxResult result = MessageBox.Show("Szeretné újrakezdeni ? Válasszon az alábbi lehetőségek közül!", "*** Game Over ***", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    StartGame();
                    break;
                case MessageBoxResult.No:
                    this.Close();
                    break;
            }
        }
    }
}
