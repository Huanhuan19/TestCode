using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Light
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker _backgroundWorker = new BackgroundWorker();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(200);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                _backgroundWorker.RunWorkerAsync();
            };
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblMessage.Content = "终止";
            }
            else if (e.Error!=null)
            {
                lblMessage.Content = "异常";
            }
            else
            {
                lblMessage.Content = "运行";
            }
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            TrafficLight lLight = (TrafficLight)this.FindResource("myTrafficLight");
            if (lLight.State==TrafficLight.States.GREEN)
            {
                lLight.State = TrafficLight.States.YELLOW;
                System.Threading.Thread.Sleep(3000);
            }
            else if (lLight.State==TrafficLight.States.RED)
            {
                lLight.State = TrafficLight.States.GREEN;
                System.Threading.Thread.Sleep(10000);
            }
            else if (lLight.State == TrafficLight.States.YELLOW)
            {
                lLight.State = TrafficLight.States.RED;
                System.Threading.Thread.Sleep(10000);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();
            flagEllipse.Fill = Brushes.Red;

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            lblMessage.Content = "终止";
        }
    }
}
