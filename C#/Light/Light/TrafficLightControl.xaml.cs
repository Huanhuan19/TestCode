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

namespace Light
{
    /// <summary>
    /// TrafficLightControl.xaml 的交互逻辑
    /// </summary>
    public partial class TrafficLightControl : UserControl
    {
        public TrafficLightControl()
        {
            InitializeComponent();
        }
    }
    public class TrafficLight : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private States _state;

        public enum States
        {
            GREEN,
            YELLOW,
            RED
        }

        public States State
        {
            get
            {
                return _state;
            }
            set
            {
                if (value != _state)
                {
                    _state = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("State"));
                    }
                }
            }
        }
    }
    //[ValueConversion(typeof(TrafficLight.States), typeof(Brush))]
    public class ColorConverter : IValueConverter
    {
        public enum Lights
        {
            GREEN,
            YELLOW,
            RED
        }
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            TrafficLight.States state = (TrafficLight.States)value;
            Lights light = (Lights)Enum.Parse(typeof(Lights), (string)parameter);
            switch (state)
            {
                case TrafficLight.States.GREEN:
                    if (light == Lights.GREEN)
                    {
                        return new SolidColorBrush(Colors.Green);
                    }
                    break;
                case TrafficLight.States.YELLOW:
                    if (light == Lights.YELLOW)
                    {
                        return new SolidColorBrush(Colors.Yellow);
                    }
                    break;
                case TrafficLight.States.RED:
                    if (light == Lights.RED)
                    {
                        return new SolidColorBrush(Colors.Red);
                    }
                    break;
            }
            return new SolidColorBrush(Colors.LightGray);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
