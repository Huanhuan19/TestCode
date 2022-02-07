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
using GalaSoft.MvvmLight;

namespace Support
{
    /// <summary>
    /// SupptorControl.xaml 的交互逻辑
    /// </summary>
    public partial class SupptorControl : UserControl
    {
        public SupptorControl()
        {
            InitializeComponent();
        }
    }

    public class LightColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            bool result = (bool)value;
            if (result)
            {

                return new SolidColorBrush(Colors.Green);


            }
            else
            {
                return new SolidColorBrush(Colors.Gray);
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class SupptorControlVM : ViewModelBase
    {
        private bool isLight;
        public bool IsLighten
        {
            get { return isLight; }
            set
            {
                isLight = value;
                RaisePropertyChanged();
            }
        }

        private string supportNo;
        public string SupportNo
        {
            get { return supportNo; }
            set
            {
                supportNo = value;
                RaisePropertyChanged();
            }
        }
    }
}
