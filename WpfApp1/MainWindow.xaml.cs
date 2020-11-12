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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer;
        bool playing = false;
        DateTime startTime = DateTime.Now;
        DateTime stopTime = DateTime.Now;
        DateTime pressTime = DateTime.Now;
        TimeSpan deltaPressTime = TimeSpan.FromMilliseconds(500);
        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();

            timer.Tick += new EventHandler(timer_Tick);

            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);

            playBtnLabel.Text = Properties.Resources.PlayBtn;
            resetTimeText();
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime tm = DateTime.Now;
            //Защита от случайного многократного нажатия
            if (tm - pressTime < deltaPressTime)
                return;
            pressTime = tm;
            playing = !playing;

            if (playing)
            {
                startTime =tm;
                timer.Start();
                playBtnLabel.Text = Properties.Resources.StopBtn;


            }
            else{
                stopTime = tm;
                timer.Stop();
                playBtnLabel.Text = Properties.Resources.PlayBtn;
            }

            resetTimeText();
        }

        private void resetTimeText()
        {
            TimeSpan dist;
            if (playing)
            {
                dist = DateTime.Now - startTime;
            }
            else
            {
                dist = stopTime - startTime;
            }

            timerLabel.Content = dist.ToString(@"mm\:ss\.ff");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            resetTimeText();
        }
    }
}
